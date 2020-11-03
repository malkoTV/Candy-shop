using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVariables
{
    private static int playerSpeed = 25;
    private static int poolCapacity = 3;
    private static int candySpeed = 20;
    private static float shootSpeed = 0.5f;
    private static int candySpawnDelay = 4;
    private static float customerSpawnDelay = 3f;
    private static float customerSpeed = 2f;
    private static float forceDelay = 3f;
    private static int orderMax = 5;
    private static float boundary = 6f;
    private static int price = 10;
    private static int winScore = 10;
    private static int customersLeft = 5;

    private static GameObject canvasObj;

    public static GameObject CanvasObj
    {
        get { return canvasObj; }
        set { canvasObj = value; }
    }

    // saved for efficient boundary checking
    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;

    public static float ScreenLeft
    {
        get { return screenLeft; }
    }

    public static float ScreenRight
    {
        get { return screenRight; }
    }

    public static float ScreenTop
    {
        get { return screenTop; }
    }

    public static float ScreenBottom
    {
        get {return screenBottom; }
    }
    public static int PlayerSpeed
    {
        get { return playerSpeed; }
        set { playerSpeed = value; }
    }

    public static int PoolCapacity
    {
        get { return poolCapacity; }
    }

    public static int CandySpeed
    {
        get { return candySpeed; }
        set { candySpeed = value; }
    }

    public static float ShootSpeed
    {
        get { return shootSpeed; }
        set { shootSpeed = value; }
    }

    public static int CandySpawnDelay
    {
        get { return candySpawnDelay; }
        set { candySpawnDelay = value; }
    }

    public static float CustomerSpawnDelay
    {
        get { return customerSpawnDelay; }
        set { customerSpawnDelay = value; }
    }

    public static float CustomerSpeed
    {
        get { return customerSpeed; }
    }

    public static float ForceDelay
    {
        get { return forceDelay; }
    }

    public static int OrderMax
    {
        get { return orderMax; }
    }

    public static float Boundary
    {
        get { return boundary; }
    }

    public static int Price
    {
        get { return price; }
        set
        {
            if (value > 0)
            {
                price = value;
            }
        }
    }

    public static int WinScore
    {
        get { return winScore; }
    }

    public static int CustomersLeft
    {
        get { return customersLeft; }
    }

    public static void Initialize()
    {
        // save screen edges in world coordinates
        float screenZ = -Camera.main.transform.position.z;
        Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        Vector3 upperRightCornerScreen = new Vector3(
            Screen.width, Screen.height, screenZ);
        Vector3 lowerLeftCornerWorld =
            Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        Vector3 upperRightCornerWorld =
            Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        screenLeft = lowerLeftCornerWorld.x;
        screenRight = upperRightCornerWorld.x;
        screenTop = upperRightCornerWorld.y;
        screenBottom = lowerLeftCornerWorld.y;
    }
}
