using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    private Transform target;  // Object that this label should follow
    private static Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default
    private Camera cam;
    private Vector3 pos;

    private static GameObject orderPrefab;

    public static GameObject OrderPrefab
    {
        set { orderPrefab = value; }
    }

    public static Vector3 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    void Awake()
    {
        cam = Camera.main;
    }
    
    public void Initialize(Transform target)
    {
        this.target = target;
        List<PooledObjectName> orders = target.gameObject.GetComponent<Customer>().Orders;

        //set background to size
        RectTransform rt = GetComponent<RectTransform>();

        //determine offset for each item
        Vector2 deltaPos = new Vector2(rt.sizeDelta.x, 0);
        Vector2 startPos = new Vector2(
            rt.position.x - (rt.sizeDelta.x * (int)(orders.Count / 2)), rt.position.y);
        
        if(orders.Count % 2 == 0)
        {
            startPos.x += (deltaPos.x / 2);
        }

        rt.sizeDelta = new Vector2(rt.sizeDelta.x * orders.Count, rt.sizeDelta.x);

        for (int i = 0; i < orders.Count; i++)
        {
            GameObject item = Instantiate(orderPrefab, startPos, transform.rotation, transform);
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

    void RemoveItem()
    {
        //when the customer eats order decreases
    }
}
