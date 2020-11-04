using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Customer : IntEventInvoker
{
    private List<PooledObjectName> order;
    private List<GameObject> orderObj;
    //private static Dictionary<PooledObjectName, Sprite> sprites = new Dictionary<PooledObjectName, Sprite>();
    private Sprite sprite;
    private int coins = 0;
    private static int customersLost = 0;
    private Timer forceTimer;

    private Rigidbody2D rb2d;
    
    public Sprite Sprite
    {
        set { sprite = value; }
    }

    public List<PooledObjectName> Orders
    {
        get { return order; }
    }

    public static void LoadSprites()
    {
        //run this method once when initializing the game
        //sprites.Add
    }

    void Initialize()
    {
        rb2d = GetComponent<Rigidbody2D>();

        unityEvents.Add(EventName.OrderCompletedEvent, new OrderCompletedEvent());
        EventManager.AddInvoker(EventName.OrderCompletedEvent, this);

        unityEvents.Add(EventName.GameLostEvent, new GameLostEvent());
        EventManager.AddInvoker(EventName.GameLostEvent, this);

        int size = UnityEngine.Random.Range(1, GlobalVariables.OrderMax + 1);
        order = new List<PooledObjectName>(size);

        int[] values = (int[])Enum.GetValues(typeof(PooledObjectName));

        for (int i = 0; i < size; i++)
        {
            int idx = UnityEngine.Random.Range(values[0], values[values.Length - 1] + 1);
            PooledObjectName objName = (PooledObjectName)Enum.Parse(typeof(PooledObjectName), "" + idx);
            order.Add(objName);
        }

        GameObject prefabObject = (GameObject)Resources.Load("Order canvas");
        
        Vector3 offsetPos = Vector3.up;
        offsetPos.y += (GetComponent<BoxCollider2D>().size.y * 15);
        //offsetPos = Camera.main.WorldToScreenPoint(offsetPos);
        Order.Offset = offsetPos;


        Vector3 pos = Camera.main.WorldToScreenPoint(Camera.main.transform.TransformPoint(transform.position)); 
        pos.y += (GetComponent<BoxCollider2D>().size.y / 2);

        GameObject panelObj = Instantiate(prefabObject, new Vector3(0, GlobalVariables.ScreenTop, 0), transform.rotation);
        panelObj.transform.SetParent(this.gameObject.transform, true);
        panelObj.transform.GetChild(0).gameObject.GetComponent<Order>().Initialize(gameObject.transform); 

        coins = size * GlobalVariables.Price;

        PrintOrder();

        forceTimer = gameObject.AddComponent<Timer>();
        forceTimer.Duration = GlobalVariables.ForceDelay;
        forceTimer.AddTimerFinishedEventListener(Movement);
        forceTimer.Run();
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
        foreach(PooledObjectName item in order)
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
            if (order.Contains(candyName))
            {
                order.RemoveAt(order.IndexOf(candyName));
                if(order.Count == 0)
                {
                    unityEvents[EventName.OrderCompletedEvent].Invoke(coins);
                    Destroy(gameObject);
                }
                PrintOrder();
                Debug.Log("Satisfyed customer");
            }
            else
            {
                Debug.Log("Angry customer");
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
        //rb2d.AddForce(Vector2.down * GlobalVariables.CustomerSpeed, ForceMode2D.Force);
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
