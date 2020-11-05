using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CandySpawner : MonoBehaviour
{
    Timer spawnTimer;
    List<GameObject> candies = new List<GameObject>();

    void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = GlobalVariables.CandySpawnDelay;
        spawnTimer.AddTimerFinishedEventListener(SpawnNewObject);
        spawnTimer.Run();
    }

    void SpawnNewObject()
    {
        spawnTimer.Run();
        int[] values = (int[])Enum.GetValues(typeof(PooledObjectName));
        int i = UnityEngine.Random.Range(values[0], values[values.Length - 1] + 1);
        PooledObjectName objName = (PooledObjectName)Enum.Parse(typeof(PooledObjectName), "" + i);
        GameObject candy = ObjectPool.GetPooledObject(objName);

        candy.transform.position = transform.position;
        candy.SetActive(true);
        candy.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void PickUpObject(GameObject obj)
    {

    }
}
