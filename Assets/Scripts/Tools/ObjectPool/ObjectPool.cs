using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������⣺
//1.Ƶ����ʵ������������һ�������ܿ���
//2.��������ٻ���ɴ������ڴ������������GC��Ƶ���������������ڴ��ͷŴ����Ŀ��ٸ�
//˼�룺
//ͨ���ظ������Ѿ������Ķ��󣬱���Ƶ���Ĵ��������ٹ��̣�����ϵͳ���ڴ������������մ����Ŀ���
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
