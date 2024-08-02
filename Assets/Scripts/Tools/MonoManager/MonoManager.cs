using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager :MonoSingleton<MonoManager>
{
    private event UnityAction updateEvent;
    private event UnityAction fixedUpdateEvent;
    private event UnityAction lateUpdateEvent;

    public void AddUpdateListener(UnityAction updateFun) 
    {
    updateEvent += updateFun;
    }
      public void AddFixedUpdateListener(UnityAction fixedUpdateFun) 
    {
    fixedUpdateEvent += fixedUpdateFun;
    }
      public void AddLateUpdateListener(UnityAction lateUpdateFun) 
    {
    lateUpdateEvent += lateUpdateFun;
    }

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
