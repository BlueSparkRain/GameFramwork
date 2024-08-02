using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//公共Mono管理器,可以让未继承Mono的脚本实现帧更新或协程功能
//外部（应用端）调用时向三种事件中添加应用端自身所需的帧更新函数即可
public class MonoManager :MonoSingleton<MonoManager>
{
    private event UnityAction updateEvent;
    private event UnityAction fixedUpdateEvent;
    private event UnityAction lateUpdateEvent;

    ////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 添加update帧更新监听函数
    /// </summary>
    /// <param name="updateFun"></param>
    public void AddUpdateListener(UnityAction updateFun) 
    {
    updateEvent += updateFun;
    }

    /// <summary>
    /// 添加fixedUpdate帧更新监听函数
    /// </summary>
    /// <param name="fixedUpdateFun"></param>
    public void AddFixedUpdateListener(UnityAction fixedUpdateFun) 
    {
    fixedUpdateEvent += fixedUpdateFun;
    }
    /// <summary>
    /// 添加lateUpdate帧更新监听函数
    /// </summary>
    /// <param name="lateUpdateFun"></param>
    public void AddLateUpdateListener(UnityAction lateUpdateFun) 
    {
    lateUpdateEvent += lateUpdateFun;
    }
    ////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// 移除update帧更新监听函数
    /// </summary>
    /// <param name="updateFun"></param>
    public void RemoveUpdateListener(UnityAction updateFun) 
    {
        updateEvent -= updateFun;
    }

    /// <summary>
    /// 移除fixedUpdate帧更新监听函数
    /// </summary>
    /// <param name="fixedUpdateFun"></param>
    public void RemoveFixedUpdateListener(UnityAction fixedUpdateFun) 
    {
        fixedUpdateEvent -= fixedUpdateFun;
    }

    /// <summary>
    /// 移除lateUpdate帧更新监听函数
    /// </summary>
    /// <param name="lateUupdateFun"></param>
    public void RemoveLateUpdateListener(UnityAction lateUupdateFun) 
    {
        lateUpdateEvent -= lateUupdateFun;
    }
   /////////////////////////////////////////////////////////////////////////////////////////
   //在此处调用执行外部的委托
    void Update()
    {
        updateEvent?.Invoke();
    }
    private void FixedUpdate()
    {
        fixedUpdateEvent?.Invoke();
    }

    private void LateUpdate()
    {
        lateUpdateEvent?.Invoke();
    }
}
