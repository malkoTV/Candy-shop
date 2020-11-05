using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<CandySpawner> spawners;

    void Awake()
    {
        GlobalVariables.Initialize();
        ObjectPool.Initialize();

        spawners = new List<CandySpawner>();
        GameObject[] spawnerObj = GameObject.FindGameObjectsWithTag("Spawner");

        foreach(var item in spawnerObj)
        {
            spawners.Add(item.AddComponent<CandySpawner>());
        }

        EventManager.Initialize();

        Customer.OrderBackground = (GameObject)Resources.Load("Order canvas");
        Order.OrderPrefab = (GameObject)Resources.Load("Order prefab");
    }
}
