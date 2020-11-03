using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    Timer spawnTimer;

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GlobalVariables.CustomerSpawnDelay;
        spawnTimer.AddTimerFinishedEventListener(SpawnNewObject);
        spawnTimer.Run();
    }

    void SpawnNewObject()
    {
        GameObject obj = Instantiate((GameObject)Resources.Load("Customer prefab"));
        obj.transform.position = new Vector3(Random.Range(GlobalVariables.ScreenLeft 
            + GlobalVariables.Boundary, GlobalVariables.ScreenRight 
            - GlobalVariables.Boundary + 1), GlobalVariables.ScreenTop, 0);
        spawnTimer.Run();
    }
}
