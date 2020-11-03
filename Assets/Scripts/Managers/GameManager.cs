using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<CandySpawner> spawners;

    // Start is called before the first frame update
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

        Order.LoadSprites();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
