using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CandySpawner : MonoBehaviour
{
    Timer spawnTimer;
    List<GameObject> candies = new List<GameObject>();
    int[] values;

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        values = (int[])Enum.GetValues(typeof(PooledObjectName));

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GlobalVariables.CandySpawnDelay;
        spawnTimer.AddTimerFinishedEventListener(SpawnNewObject);
        spawnTimer.Run();
    }

    void SpawnNewObject()
    {
        spawnTimer.Run();
        if (candies.Count < GlobalVariables.ContainerMax)
        {
            int i = UnityEngine.Random.Range(values[0], values[values.Length - 1] + 1);
            PooledObjectName objName = (PooledObjectName)Enum.Parse(typeof(PooledObjectName), "" + i);
            GameObject candy = ObjectPool.GetPooledObject(objName);

            candy.transform.position = transform.position;
            candy.SetActive(true);
            candy.GetComponent<Rigidbody2D>().gravityScale = 1;

            candies.Add(candy);
        }
    }

}
