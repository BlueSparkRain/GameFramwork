using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class EventCenter : BaseSingletonManager<EventCenter>
{
    /// <summary>
    /// EventCenter主要作用：降低程序耦合度，使不同模块隔离，不需要直接引用或依赖彼此的具体实现
    /// 基本原理：中心化机制，观察者模式，松耦合通信
    /// 关键方法：
    /// 1.触发（分发）事件
    /// 2.添加、移除事件监听者
    /// 3.清除所有事件监听者
    /// </summary>\
    /// 
    private EventCenter() { }
    
    private Dictionary<E_EventType, IEventInfo> eventDic = new Dictionary<E_EventType, IEventInfo>();

    /*无参****************************************************************/

    public void AddEventListener(E_EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            //因为是父类（IEventInfo）装子类（EventInfo），先as为子类（EventInfo），再使用其中的actions
            (eventDic[name] as EventInfo).actions += action;
        else
            eventDic.Add(name, new EventInfo(action));
    }

    public void EventTrigger(E_EventType name)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions?.Invoke();
    }

    public void RemoveEventListener(E_EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo).actions -= action;
    }
    //无参Priority
    public void AddEventListener(E_EventType name, PriorityAction priorityAction) 
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as PriorityEventInfo).GetNewEvent(priorityAction);   
        else
            eventDic.Add(name, new PriorityEventInfo(priorityAction));
    }

    public void EventTrigger_Priority(E_EventType name) 
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as PriorityEventInfo).allActions.ForEach(val => { val.Action?.Invoke(); });
    }

    public void RemoveEventListener(E_EventType name, PriorityAction priorityAction)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as PriorityEventInfo).allActions.Remove(priorityAction);
    }

    /*有参****************************************************************/
    public void AddEventListener<T>(E_EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions += action;
        else
            eventDic.Add(name, new EventInfo<T>(action));
    }

    public void EventTrigger<T>(E_EventType name, T info)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
    }
    public void RemoveEventListener<T>(E_EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as EventInfo<T>).actions -= action;
    }

    //有参Priority
    public void AddEventListener<T>(E_EventType name, PriorityAction<T> priorityAction) 
    {

        if (eventDic.ContainsKey(name))
            (eventDic[name] as PriorityEventInfo<T>).GetNewEvent(priorityAction);
        else
            eventDic.Add(name, new PriorityEventInfo<T>(priorityAction));
    }

    public void EventTrigger_Priority<T>(E_EventType name, T info)
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as PriorityEventInfo<T>).allActions.ForEach(val => { val.Action?.Invoke(info); });       
    }

    public void RemoveEventListener<T>(E_EventType name,PriorityAction<T> priorityAction) 
    {
        if (eventDic.ContainsKey(name))
            (eventDic[name] as PriorityEventInfo<T>).allActions.Remove(priorityAction);
    }


    /// <summary>
    /// 清除所有事件
    /// </summary>
    public void ClearAllEvents()
    {
        eventDic.Clear();
    }
}


/// <summary>
/// 有参自定义优先级委托类型
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityAction<T>
{
    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority;

    public UnityAction<T> Action;

    public PriorityAction(int priority, UnityAction<T> action)
    {
        Priority = priority;
        Action = action;
    }
}

/// <summary>
/// 有参优先级事件集合类型
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityEventInfo<T> : IEventInfo
{
    public List<PriorityAction<T>> allActions = new List<PriorityAction<T>>();

    //真正观察者 对应的 函数信息 记录在其中
    public PriorityEventInfo(PriorityAction<T> action)
    {
        allActions.Add(action);
    }

    public void GetNewEvent(PriorityAction<T> action)
    {
        allActions.Add(action);
        //降序排列
        allActions.Sort((x, y) => y.Priority.CompareTo(x.Priority));
    }
}

/// <summary>
/// 无参自定义优先级委托类型
/// </summary>
public class PriorityAction 
{
    /// <summary>
    /// 优先级
    /// </summary>
    public int Priority;

    public UnityAction  Action;

    public PriorityAction(int priority, UnityAction action) 
    {
        Priority = priority;
        Action = action;
    }
}

/// <summary>
/// 无参优先级事件集合类型
/// </summary>
public class PriorityEventInfo : IEventInfo
{
    public List<PriorityAction> allActions=new List<PriorityAction>();

    //真正观察者 对应的 函数信息 记录在其中
    public PriorityEventInfo(PriorityAction action)
    {
        allActions.Add(action);
    }

    public void GetNewEvent(PriorityAction action) 
    {
        allActions.Add(action);
        //降序排列
        allActions.Sort((x, y) => y.Priority.CompareTo(x.Priority));
    }
}


//是为了包裹对应观察者 函数委托类
public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public List<PriorityAction> allActions;

    //真正观察者 对应的 函数信息 记录在其中
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}


//此接口用于里氏替换原则 装载子类的 父类，目标调父执行子中的
public interface IEventInfo{}

public enum E_EventType 
{
    /// <summary>
    /// 参数：player
    /// </summary>
    E_pkayerDead,
    E_sceneLoad,
    E_dataSave,
    E_Test,
}




