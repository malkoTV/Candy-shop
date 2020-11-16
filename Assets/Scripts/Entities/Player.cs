using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // collider dimensions
    float colliderHalfWidth = 4f;
    float boundary;

    private bool canShoot = true;
    Timer cooldownTimer;

    private SpriteRenderer candyShoot;

    //private Candy candyObjScript;
    private PooledObjectName candyName;

    // Start is called before the first frame update
    void Start()
    {
        candyShoot = gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        candyShoot.sprite = ObjectPool.Sprites[candyName];
        cooldownTimer = gameObject.AddComponent<Timer>();
        cooldownTimer.Duration = GlobalVariables.ShootSpeed;
        boundary = GlobalVariables.Boundary;
    }
       
    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }

    public void CandyPicked(PooledObjectName name)
    {
        candyName = name;
        candyShoot.sprite = ObjectPool.Sprites[candyName];
    }

    void Movement()
    {
        Vector3 position = transform.position;
        float horizontalInput = Input.GetAxis("Horizontal");
       
        if(horizontalInput != 0)
        {
            position.x += horizontalInput * GlobalVariables.PlayerSpeed * Time.deltaTime;
        }

        transform.position = position;
        ClampInScreen();
    }

    void Shoot()
    {
        Vector3 position = transform.position;
        if (position.x - boundary < GlobalVariables.ScreenLeft || 
            position.x + boundary > GlobalVariables.ScreenRight)
        {
            canShoot = false;
            return;
        }

        if (!canShoot && Input.GetAxisRaw("Jump") == 0)
        {
            if(candyName != null)
            {
                cooldownTimer.Stop();
                canShoot = true;
            }
        }

        // shoot
        if (canShoot && Input.GetAxisRaw("Jump") != 0)
        {
            cooldownTimer.Run();
            canShoot = false;

            GameObject obj = ObjectPool.GetPooledObject(candyName);
            Vector3 nPos = transform.position;
            nPos.y += ((obj.GetComponent<BoxCollider2D>().size.y / 2) + (GetComponent<BoxCollider2D>().size.y) / 2) * 1.1f;
            obj.transform.position = nPos;
            obj.GetComponent<Candy>().Active();
        }
    }

    void ClampInScreen()
    {
        // check boundaries and shift as necessary
        Vector3 position = transform.position;
        if (position.x - colliderHalfWidth < GlobalVariables.ScreenLeft)
        {
            position.x = GlobalVariables.ScreenLeft + colliderHalfWidth;
        }
        if (position.x + colliderHalfWidth > GlobalVariables.ScreenRight)
        {
            position.x = GlobalVariables.ScreenRight - colliderHalfWidth;
        }
        transform.position = position;
    }
}
