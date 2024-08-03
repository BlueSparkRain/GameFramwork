using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class EventCenter : BaseSingletonManager<EventCenter>
{
    /// <summary>
    /// EventCenter主要作用：降低程序耦合度，使不同模块隔离，不需要直接引用或依赖彼此的具体实现
    /// 基本原理：中心化机制，观察者模式，松耦合通信
    /// 关键方法：
    /// 1.触发（分发）事件
    /// 2.添加、移除事件监听者
    /// 3.清除所有事件监听者
    /// </summary>
    private EventCenter() { }
    
    private Dictionary<E_EventType, IEventInfo> eventDic = new Dictionary<E_EventType, IEventInfo>();


    /*无参****************************************************************/

    public void AddEventListener(E_EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            //因为是父类（IEventInfo）装子类（EventInfo），先as为子类（EventInfo），再使用其中的actions
            (eventDic[name] as EventInfo).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo(action));
          

        }
    }

    public void EventTrigger(E_EventType name)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions?.Invoke();
        }

    }

    public void RemoveEventListener(E_EventType name, UnityAction action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo).actions -= action;
        }
    }

    
   /*有参****************************************************************/
    public void AddEventListener<T>(E_EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            //因为是父类（IEventInfo）装子类（EventInfo<T>），先as为子类（EventInfo<T>），再使用其中的actions
            (eventDic[name] as EventInfo<T>).actions += action;
        }
        else
        {
            eventDic.Add(name, new EventInfo<T>(action));
           

        }
    }
    public void EventTrigger<T>(E_EventType name, T info)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions?.Invoke(info);
        }
    }
    public void RemoveEventListener<T>(E_EventType name, UnityAction<T> action)
    {
        if (eventDic.ContainsKey(name))
        {
            (eventDic[name] as EventInfo<T>).actions -= action;
        }
    }


    public void ClearAllEvents()
    {
        eventDic.Clear();
    }

}


//是为了包裹对应观察者 函数委托的 类
public class EventInfo : IEventInfo
{
    public UnityAction actions;

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
public interface IEventInfo
{

}
    


/// <summary>
/// 添加观察者时使用变量存储字符串便于修改，减少出错的可能
/// </summary>
public static class EventCenterConstString 
{
    public const string Dead = "Deadh";
}

public enum E_EventType 
{
    /// <summary>
    /// 参数：player
    /// </summary>
    E_pkayerDead,
}


