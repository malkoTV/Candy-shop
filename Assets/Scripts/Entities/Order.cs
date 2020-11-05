using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Order : IntEventInvoker
{
    private Transform target;  // Object that this label should follow
    private static Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default
    private Camera cam;
    private Vector3 pos;

    private static GameObject orderPrefab;
    RectTransform rt;
    Vector2 rtSize;
    Vector2 deltaPos;

    private List<GameObject> itemObjs = new List<GameObject>();

    public static GameObject OrderPrefab
    {
        set { orderPrefab = value; }
    }

    public static Vector3 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    private List<PooledObjectName> orders = new List<PooledObjectName>();
    public List<PooledObjectName> Items
    {
        get { return orders; }
        set { orders = value; }
    }
    private int coins = 0;

    void Awake()
    {
        cam = Camera.main;
    }
    
    public void Initialize(Transform target)
    {
        this.target = target;

        unityEvents.Add(EventName.OrderCompletedEvent, new OrderCompletedEvent());
        EventManager.AddInvoker(EventName.OrderCompletedEvent, this);

        rt = GetComponent<RectTransform>();
        rtSize = rt.sizeDelta;

        coins = GlobalVariables.Price * orders.Count;

        for (int i = 0; i < orders.Count; i++)
        {
            GameObject item = Instantiate(orderPrefab, transform.position, transform.rotation, transform);
            item.GetComponent<Image>().sprite = ObjectPool.Sprites[orders[i]];
            itemObjs.Add(item);
        }

        //determine offset for each item
        deltaPos = new Vector2(rtSize.x, 0);

        DisplayOrder();
    }

    private void DisplayOrder()
    {
        Vector2 startPos = new Vector2(
            rt.position.x - (rtSize.x * (int)(orders.Count / 2)), rt.position.y);

        if (orders.Count % 2 == 0)
        {
            startPos.x += (deltaPos.x / 2);
        }

        rt.sizeDelta = new Vector2(rtSize.x * orders.Count, rtSize.x);

        for (int i = 0; i < orders.Count; i++)
        {
            GameObject item = itemObjs[i];
            item.GetComponent<Image>().sprite = ObjectPool.Sprites[orders[i]];
            RectTransform itemTr = item.GetComponent<RectTransform>();
            itemTr.position = startPos;

            startPos += deltaPos;
        }
    }

    void Update()
    {
        pos = cam.WorldToScreenPoint(cam.transform.TransformPoint(target.position));
        pos += offset;
        gameObject.transform.position = pos;
    }    

    public void RemoveItem(PooledObjectName name)
    {
        //when the customer eats order decreases

        //remove item from order list
        orders.RemoveAt(orders.IndexOf(name));
        //save reference to last order item obj
        GameObject toDestr = itemObjs[itemObjs.Count - 1];
        //shrink order
        itemObjs.Remove(toDestr);
        //destroy unnecessary obj
        Destroy(toDestr);

        //order is complete when there are no more items
        if (orders.Count == 0)
        {
            unityEvents[EventName.OrderCompletedEvent].Invoke(coins);
            Destroy(target.gameObject);
        }

        DisplayOrder();
    }
}
