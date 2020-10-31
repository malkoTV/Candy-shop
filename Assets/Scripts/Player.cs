using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // collider dimensions
    float colliderHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }
       

    // Update is called once per frame
    void Update()
    {
        Movement();
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
        if(Input.GetAxisRaw("Fire1") != 0)
        {
            //add timer
            Debug.Log("Shooting");
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
