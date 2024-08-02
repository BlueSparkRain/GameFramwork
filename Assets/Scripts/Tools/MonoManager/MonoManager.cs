using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//����Mono������,������δ�̳�Mono�Ľű�ʵ��֡���»�Э�̹���
//�ⲿ��Ӧ�öˣ�����ʱ�������¼������Ӧ�ö����������֡���º�������
public class MonoManager :MonoSingleton<MonoManager>
{
    private event UnityAction updateEvent;
    private event UnityAction fixedUpdateEvent;
    private event UnityAction lateUpdateEvent;

    ////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// ���update֡���¼�������
    /// </summary>
    /// <param name="updateFun"></param>
    public void AddUpdateListener(UnityAction updateFun) 
    {
    updateEvent += updateFun;
    }

    /// <summary>
    /// ���fixedUpdate֡���¼�������
    /// </summary>
    /// <param name="fixedUpdateFun"></param>
    public void AddFixedUpdateListener(UnityAction fixedUpdateFun) 
    {
    fixedUpdateEvent += fixedUpdateFun;
    }
    /// <summary>
    /// ���lateUpdate֡���¼�������
    /// </summary>
    /// <param name="lateUpdateFun"></param>
    public void AddLateUpdateListener(UnityAction lateUpdateFun) 
    {
    lateUpdateEvent += lateUpdateFun;
    }
    ////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// �Ƴ�update֡���¼�������
    /// </summary>
    /// <param name="updateFun"></param>
    public void RemoveUpdateListener(UnityAction updateFun) 
    {
        updateEvent -= updateFun;
    }

    /// <summary>
    /// �Ƴ�fixedUpdate֡���¼�������
    /// </summary>
    /// <param name="fixedUpdateFun"></param>
    public void RemoveFixedUpdateListener(UnityAction fixedUpdateFun) 
    {
        fixedUpdateEvent -= fixedUpdateFun;
    }

    /// <summary>
    /// �Ƴ�lateUpdate֡���¼�������
    /// </summary>
    /// <param name="lateUupdateFun"></param>
    public void RemoveLateUpdateListener(UnityAction lateUupdateFun) 
    {
        lateUpdateEvent -= lateUupdateFun;
    }
   /////////////////////////////////////////////////////////////////////////////////////////
   //�ڴ˴�����ִ���ⲿ��ί��
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
