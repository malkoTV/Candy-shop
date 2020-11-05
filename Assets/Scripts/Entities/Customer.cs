using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Customer : IntEventInvoker
{
    private static int customersLost = 0;

    private Order order;

    private static GameObject orderBackground;
    public static GameObject OrderBackground
    {
        set { orderBackground = value; }
    }

    void Initialize()
    {
        //add event that game lost when customers missed
        unityEvents.Add(EventName.GameLostEvent, new GameLostEvent());
        EventManager.AddInvoker(EventName.GameLostEvent, this);

        //offset order display above the customer
        Vector3 offsetPos = Vector3.up;
        offsetPos.y += (GetComponent<BoxCollider2D>().size.y * 15);
        Order.Offset = offsetPos;

        //instantiate order display
        GameObject panelObj = Instantiate(orderBackground, new Vector3(0, GlobalVariables.ScreenTop, 0), transform.rotation);
        panelObj.transform.SetParent(this.gameObject.transform, true);

        //save reference to order script
        order = panelObj.transform.GetChild(0).gameObject.GetComponent<Order>();

        int size = UnityEngine.Random.Range(1, GlobalVariables.OrderMax + 1);
        int[] values = (int[])Enum.GetValues(typeof(PooledObjectName));
        List<PooledObjectName> items = new List<PooledObjectName>(size);

        //fill the order with random items
        for (int i = 0; i < size; i++)
        {
            int idx = UnityEngine.Random.Range(values[0], values[values.Length - 1] + 1);
            PooledObjectName objName = (PooledObjectName)Enum.Parse(typeof(PooledObjectName), "" + idx);
            items.Add(objName);
        }
        order.Items = items;

        //initialize order
        order.Initialize(gameObject.transform);
    }

    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void PrintOrder()
    {
        string str = "Order: ";
        foreach(PooledObjectName item in order.Items)
        {
            str += (item + ", ");
        }

        Debug.Log(str);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Candy"))
        {
            PooledObjectName candyName = other.gameObject.GetComponent<Candy>().CandyName;
            if (order.Items.Contains(candyName))
            {
                order.RemoveItem(candyName);
            }
        }
        else if(other.gameObject.CompareTag("Player"))
        {
            unityEvents[EventName.GameLostEvent].Invoke(1);
        }
    }

    void Movement()
    {
        transform.Translate(Vector2.down * Time.deltaTime * GlobalVariables.CustomerSpeed);
    }

    void OnBecameInvisible()
    {
        customersLost++;
        if(customersLost >= GlobalVariables.CustomersLeft)
        {
            unityEvents[EventName.GameLostEvent].Invoke(1);
        }
        Destroy(gameObject);
    }
}
