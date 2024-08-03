using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// 自动挂载式的继承Mono的单例脚本基类，推荐使用，无需手动挂载，无需考虑切场问题，动态添加，
/// 尽量不要手动挂载
/// </summary>
/// <typeparam name="T"></typeparam>
[DisallowMultipleComponent]//禁止在同一个gamobject上挂载多个相同此脚本
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //手动挂载式
                instance = FindAnyObjectByType<T>();
                //自动挂载式
                if (instance == null)
                {
                //非挂载式单例在被调用时直接创建
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

        //手动挂载式初始化
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


