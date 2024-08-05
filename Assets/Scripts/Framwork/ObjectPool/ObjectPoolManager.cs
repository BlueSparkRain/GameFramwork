using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager : MonoSingleton<ObjectPoolManager>
{

    private Dictionary<string, ObjectPool> poolDic = new Dictionary<string, ObjectPool>();

    protected override void Awake()
    {
        base.Awake();
        
    }

    public void AddObjectPoolListener(string poolName,ObjectPool newPool) 
    {
      if (!poolDic.ContainsKey(poolName)) 
      {
            poolDic.Add(poolName, newPool);
            newPool.transform.SetParent(transform);
            newPool.name = poolName;
            print("Ìí¼ÓÐÂµÄobjectpool:" + poolName);
      }
    
    }

    public void RemoveObjectPoolListener(string poolName) 
    {
        poolDic.Remove(poolName);

    }

}
