using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//[RequireComponent(typeof(GUIText))]
public class Order : MonoBehaviour
{
    private Transform target;  // Object that this label should follow
    private static Vector3 offset = Vector3.up;    // Units in world space to offset; 1 unit above object by default
    Camera cam;
    Transform camTransform;

    Vector3 pos;

    private static Dictionary<PooledObjectName, Sprite> candySprites
        = new Dictionary<PooledObjectName, Sprite>();

    //use sprites from object pool
    public static void LoadSprites()
    {
        var values = Enum.GetValues(typeof(PooledObjectName));
        foreach (var item in values)
        {
            PooledObjectName objName = (PooledObjectName)Convert.ChangeType(item, typeof(PooledObjectName));
            candySprites.Add(objName, Resources.Load<Sprite>("Sprites/" + objName));
        }
    }

    public static Vector3 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    private GameObject panel;

    void Start()
    {
        cam = Camera.main;
        camTransform = cam.transform;
    }
    
    public void Initialize(Transform target)
    {
        this.target = target;
        List<PooledObjectName> orders = target.gameObject.GetComponent<Customer>().Orders;

        //set background to size
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x * orders.Count, rt.sizeDelta.x);

        //make it an if statement from -2 to +2 approx to position all the elements
        foreach(PooledObjectName item in orders)
        {
            //instantiate
            //offset position
            //make a child
        }

        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = candySprites[orders[0]];
        panel = gameObject; 
    }

    void Update()
    {
        pos = cam.WorldToScreenPoint(camTransform.TransformPoint(target.position));
        pos += offset;
        panel.transform.position = pos;
    }    

    void RemoveItem()
    {
        //when the customer eats order decreases
    }
}
