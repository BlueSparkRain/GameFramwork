using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// �Զ�����ʽ�ļ̳�Mono�ĵ����ű����࣬�Ƽ�ʹ�ã������ֶ����أ����迼���г����⣬��̬��ӣ�
/// ������Ҫ�ֶ�����
/// </summary>
/// <typeparam name="T"></typeparam>
[DisallowMultipleComponent]//��ֹ��ͬһ��gamobject�Ϲ��ض����ͬ�˽ű�
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //�ֶ�����ʽ
                instance = FindAnyObjectByType<T>();
                //�Զ�����ʽ
                if (instance == null)
                {
                //�ǹ���ʽ�����ڱ�����ʱֱ�Ӵ���
                  GameObject newManager=new GameObject(typeof(T) + "SingleManager");
                    instance = newManager.AddComponent<T>();
                    // instance.Init(newManager);
                    DontDestroyOnLoad(newManager);
                }
            }  
            return instance;
        }
        private set { }
    }
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        //�ֶ�����ʽ��ʼ��
        if (instance == null)
        {
            instance = this as T;
        }
        DontDestroyOnLoad (this.gameObject);
    }

    public virtual void Init(GameObject newManager)
    {
    }
}


