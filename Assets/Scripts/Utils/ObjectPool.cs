using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool : MonoBehaviour
{
    static GameObject prefab;
    static Dictionary<PooledObjectName, Sprite> sprites;
    static List<GameObject> pool;

    public static Dictionary<PooledObjectName, Sprite> Sprites
    {
        get { return sprites; }
    }

    public static void Initialize()
    {
        PooledObjectName objName;

        objName = PooledObjectName.Cake;
        prefab = (GameObject)Resources.Load("" + objName);
        sprites = new Dictionary<PooledObjectName, Sprite>();
        pool = new List<GameObject>(GlobalVariables.PoolCapacity);

        LoadSprites();
        
        for (int i = 0; i < GlobalVariables.PoolCapacity; i++)
        {
            pool.Add(GetNewObject(objName));
        }
    }

    private static void LoadSprites()
    {
        var values = Enum.GetValues(typeof(PooledObjectName));
        foreach (var item in values)
        {
            PooledObjectName objName = (PooledObjectName)Convert.ChangeType(item, typeof(PooledObjectName));
            sprites.Add(objName, Resources.Load<Sprite>("Sprites/" + objName));
        }
    }

    static GameObject GetNewObject(PooledObjectName name)
    {
        GameObject obj = GameObject.Instantiate(prefab);
        obj.GetComponent<Candy>().Initialize(name);
        obj.SetActive(false);
        //GameObject.DontDestroyOnLoad(obj);
        return obj;
    }

    public static GameObject GetPooledObject(PooledObjectName name)
    {
        GameObject obj;

        if(pool.Count > 0)
        {
            obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        else
        {
            pool.Capacity++;
            obj = GetNewObject(name);
        }
        return obj;
    }

    public static void ReturnPooledObject(PooledObjectName name,
        GameObject obj)
    {
        obj.GetComponent<Candy>().Inactive();
        pool.Add(obj);
    }
}
