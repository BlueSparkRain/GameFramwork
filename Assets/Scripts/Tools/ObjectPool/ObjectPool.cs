using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


//解决问题：
//1.频繁的实例化对象会带来一定的性能开销
//2.对象的销毁会造成大量的内存垃圾，会造成GC的频繁触发，导致有内存释放带来的卡顿感
//思想：
//通过重复利用已经创建的对象，避免频繁的创建和销毁过程，减少系统的内存分配和垃圾回收带来的开销
public class ObjectPool : MonoBehaviour,IObjectPoolTools
{
    [SerializeField] private string myPoolName;
    [SerializeField] private float poolSize = 20;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private GameObject poolPrefab;
    [SerializeField] private float maxCapcity=40;

    //正在使用的InstanceList，是为了复用先取出的Instance，优化性能减少内存压力
    //注意使用场景：不适合像塔防等（实例怪物不应被中途抹除）的场景
    private List<GameObject> usingInstancePool = new List<GameObject>();

    private void Awake()
    {
       ObjectPoolManager.Instance.AddObjectPoolListener(myPoolName,this);
       FullObjectPool();
    }

    /// <summary>
    /// 游戏开始时初始化池库
    /// </summary>
    public void FullObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(CreatInstance());
        }
    }

    /// <summary>
    /// 创造Instance
    /// </summary>
    /// <returns></returns>
    GameObject CreatInstance()
    {
        GameObject newInstance = Instantiate(poolPrefab, transform.position, Quaternion.identity, transform);
        newInstance.name = pool.Count.ToString();
        newInstance.SetActive(false);
        return newInstance;
    }

    /// <summary>
    /// 从池库中取出Instance
    /// </summary>
    /// <returns></returns>
    public GameObject GetInstanceFromPool()
    {
        //Seek初始池库
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                InstanceIntoUse(pool[i]);
               
                return pool[i];
            }
        }
        //扩增池库
        if (pool.Count+1 <= maxCapcity)
        {
            var newinstnce = CreatInstance();
            pool.Add(newinstnce);
            InstanceIntoUse(newinstnce);
           
            return newinstnce;
        }
        else 
        {
            ReturnPool(usingInstancePool[0]);
            return usingInstancePool[0];
       
        }

    }

    /// <summary>
    /// 显现并添加正在使用的Instance列表
    /// </summary>
    /// <param name="instance"></param>
    void InstanceIntoUse(GameObject instance) 
    {
        instance.SetActive(true);
        usingInstancePool.Add(instance);
    }

    /// <summary>
    /// 返回Instance至池中
    /// </summary>
    /// <param name="instance"></param>
    public void ReturnPool(GameObject instance)
    {
        //ReSet逻辑
        ResetInstance();
        instance.SetActive(false);

    }


    public void ResetInstance()
    {
        throw new System.NotImplementedException();
    }
}
