﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    static Dictionary<PooledObjectName, GameObject> prefabs;
    static Dictionary<PooledObjectName, List<GameObject>> pools;

    public static void Initialize()
    {
        prefabs = new Dictionary<PooledObjectName, GameObject>();
        pools = new Dictionary<PooledObjectName, List<GameObject>>();

        LoadSprites();

        var values = Enum.GetValues(typeof(PooledObjectName));
        foreach (var item in values)
        {
            PooledObjectName objName = (PooledObjectName)Convert.ChangeType(item, typeof(PooledObjectName));
            pools.Add(objName, new List<GameObject>(GlobalVariables.PoolCapacity));
            for(int i = 0; i < GlobalVariables.PoolCapacity; i++)
            {
                pools[objName].Add(GetNewObject(objName));
            }
        }
    }

    private static void LoadSprites()
    {
        var values = Enum.GetValues(typeof(PooledObjectName));
        foreach (var item in values)
        {
            PooledObjectName objName = (PooledObjectName)Convert.ChangeType(item, typeof(PooledObjectName));
            prefabs.Add(objName, Resources.Load<GameObject>("" + objName));
        }
    }

    static GameObject GetNewObject(PooledObjectName name)
    {
        GameObject obj = GameObject.Instantiate(prefabs[name]);
        obj.GetComponent<Candy>().Initialize();
        obj.SetActive(false);
        GameObject.DontDestroyOnLoad(obj);
        return obj;
    }
}
