using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//解决问题：
//1.频繁的实例化对象会带来一定的性能开销
//2.对象的销毁会造成大量的内存垃圾，会造成GC的频繁触发，导致有内存释放带来的卡顿感
//思想：
//通过重复利用已经创建的对象，避免频繁的创建和销毁过程，减少系统的内存分配和垃圾回收带来的开销
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private string myPoolName;
    [SerializeField] private float poolSize = 10;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private GameObject poolPrefab;

    private void Awake()
    {
       ObjectPoolManager.Instance.AddObjectPoolListener(myPoolName,this);
       FullObjectPool();
    }

    public void FullObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(CreatInstance());
        }
    }

    GameObject CreatInstance()
    {
        GameObject newInstance = Instantiate(poolPrefab, transform.position, Quaternion.identity, transform);
        newInstance.name = pool.Count.ToString();
        newInstance.SetActive(false);
        return newInstance;
    }

    public GameObject GetInstanceFromPool()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        var newinstnce = CreatInstance();
        newinstnce.SetActive(true);
        return newinstnce;

    }

    public void ReturnPool(GameObject instance)
    {
        instance.SetActive(false);

    }
}
