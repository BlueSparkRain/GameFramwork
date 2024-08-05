using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


//������⣺
//1.Ƶ����ʵ������������һ�������ܿ���
//2.��������ٻ���ɴ������ڴ������������GC��Ƶ���������������ڴ��ͷŴ����Ŀ��ٸ�
//˼�룺
//ͨ���ظ������Ѿ������Ķ��󣬱���Ƶ���Ĵ��������ٹ��̣�����ϵͳ���ڴ������������մ����Ŀ���
public class ObjectPool : MonoBehaviour,IObjectPoolTools
{
    [SerializeField] private string myPoolName;
    [SerializeField] private float poolSize = 20;
    private List<GameObject> pool = new List<GameObject>();
    [SerializeField] private GameObject poolPrefab;
    [SerializeField] private float maxCapcity=40;

    //����ʹ�õ�InstanceList����Ϊ�˸�����ȡ����Instance���Ż����ܼ����ڴ�ѹ��
    //ע��ʹ�ó��������ʺ��������ȣ�ʵ�����ﲻӦ����;Ĩ�����ĳ���
    private List<GameObject> usingInstancePool = new List<GameObject>();

    private void Awake()
    {
       ObjectPoolManager.Instance.AddObjectPoolListener(myPoolName,this);
       FullObjectPool();
    }

    /// <summary>
    /// ��Ϸ��ʼʱ��ʼ���ؿ�
    /// </summary>
    public void FullObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(CreatInstance());
        }
    }

    /// <summary>
    /// ����Instance
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
    /// �ӳؿ���ȡ��Instance
    /// </summary>
    /// <returns></returns>
    public GameObject GetInstanceFromPool()
    {
        //Seek��ʼ�ؿ�
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                InstanceIntoUse(pool[i]);
               
                return pool[i];
            }
        }
        //�����ؿ�
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
    /// ���ֲ��������ʹ�õ�Instance�б�
    /// </summary>
    /// <param name="instance"></param>
    void InstanceIntoUse(GameObject instance) 
    {
        instance.SetActive(true);
        usingInstancePool.Add(instance);
    }

    /// <summary>
    /// ����Instance������
    /// </summary>
    /// <param name="instance"></param>
    public void ReturnPool(GameObject instance)
    {
        //ReSet�߼�
        ResetInstance();
        instance.SetActive(false);

    }


    public void ResetInstance()
    {
        throw new System.NotImplementedException();
    }
}
