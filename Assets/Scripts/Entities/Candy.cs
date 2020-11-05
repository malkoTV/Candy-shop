using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    Rigidbody2D rb2d;
    private int speed;

    PooledObjectName candyName;

    public PooledObjectName CandyName
    {
        get { return candyName; }
    }

    // Start is called before the first frame update
    void Awake()
    {        
        rb2d = GetComponent<Rigidbody2D>();
        speed = GlobalVariables.CandySpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize(PooledObjectName name)
    {
        candyName = name;
        GetComponent<SpriteRenderer>().sprite = ObjectPool.Sprites[candyName];
    }

    public void Active()
    {
        gameObject.SetActive(true);
        rb2d.gravityScale = 0;

        //start moving
        rb2d.AddForce(Vector3.up * speed, ForceMode2D.Impulse);
    }

    public void Inactive()
    {
        rb2d.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
        gameObject.SetActive(false);
    }

    void OnBecameInvisible()
    {
        // return to the pool
        ObjectPool.ReturnPooledObject(candyName, this.gameObject);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().CandyName = candyName;
            Destroy(gameObject);
            //ObjectPool.ReturnPooledObject(candyName, gameObject);
        }
        else if (other.gameObject.CompareTag("Customer"))
        {
            //Debug.Log("Customer collision");
            Destroy(gameObject);
            //ObjectPool.ReturnPooledObject(candyName, gameObject);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }
}
