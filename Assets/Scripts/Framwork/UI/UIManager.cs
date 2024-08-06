using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UILayer
{
    Bottom,
    Middle,
    Top,
    System,
}

/// <summary>
/// 管理所有UI面板
/// 注意：预制体名和面板类名需保持一致
/// </summary>
public class UIManager : BaseSingletonManager<UIManager>
{
    Camera uiCamera;
    Canvas uiCanvas;
    EventSystem uiEventSystem;

    //层级父对象
    private Transform bottomLayer;
    private Transform middleLayer;
    private Transform topLayer;
    private Transform systemLayer;
    private UIManager()
    {
        //动态创建唯一的Canvas和Eventsystem；
        uiCamera = GameObject.Instantiate(Resources.Load("UI/UICamera")).GetComponent<Camera>();
        //UI摄像机不移除 
        GameObject.DontDestroyOnLoad(uiCamera.gameObject);
        //动态创建Canvas
        uiCanvas = GameObject.Instantiate(Resources.Load("UI/Canvas")).GetComponent<Canvas>();
        //设置UI摄像机
        uiCanvas.worldCamera = uiCamera;
        //过场景不移除
        GameObject.DontDestroyOnLoad(uiCanvas.gameObject);

        //找到层级父对象
        bottomLayer = uiCanvas.transform.GetChild(0);
        middleLayer = uiCanvas.transform.GetChild(1);
        topLayer = uiCanvas.transform.GetChild(2);
        systemLayer = uiCanvas.transform.GetChild(3);

        //动态创建EventSystem
        uiEventSystem = GameObject.Instantiate(Resources.Load("UI/EventSystem")).GetComponent<EventSystem>();
        GameObject.DontDestroyOnLoad(uiEventSystem.gameObject);
    }

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    /// <summary>
    /// 获取对应层级的rootlayer父对象
    /// </summary>
    /// <param name="rootLayer"></param>
    /// <returns></returns>
    public Transform GetRootLayer(E_UILayer rootLayer)
    {
        switch (rootLayer)
        {
            case E_UILayer.Bottom:
                return bottomLayer;
            case E_UILayer.Middle:
                return middleLayer;
            case E_UILayer.Top:
                return topLayer;
            case E_UILayer.System:
                return systemLayer;
            default:
                return null;
        }
    }

    /// <summary>
    /// 显示面板
    /// 注意：面板类型和面板预制体名字保持一致
    /// </summary>
    /// <typeparam name="T">面板的类型（需和预制体名字保持一致）</typeparam>
    /// <param name="panelName">面板的名字</param>
    /// <param name="layer">面板显示的层级</param>
    /// <param name="callBack">由于可能是异步加载，故通过委托回调形式将加载完成的面板传递出去进行使用</param>
    /// <param name="isSync"></param>
    public void ShowPanel<T>(E_ABPlatformType paltformType, E_UILayer layer = E_UILayer.Middle, UnityAction<T> callBack = null, bool isSync = false) where T : BasePanel
    {
        //获取面板名，预制体名和面板类名需保持一致
        string panelName = typeof(T).Name;
        BasePanel panel;
        //存在面板
        if (panelDic.ContainsKey(panelName))
        {
            panel = panelDic[panelName];
            if (!panel.gameObject.activeSelf) 
              panel.gameObject.SetActive(true);

            panel.ShowPanel();
            Debug.Log("面板已存在");
            callBack?.Invoke(panelDic[panelName] as T);
            return;
        }
        //不存在面板，先通过AB包加载资源
        GameObject panelobj = ABManager.Instance.LoadABRes<GameObject>("ui", panelName, paltformType);
        //层级的处理
        Transform rootlayer = GetRootLayer(layer);
        //获取UI组件返回出去
        if (rootlayer == null)
            rootlayer = middleLayer;
        //将面板预制件创建到对应父layer下，并保持原本的缩放大小
        panelobj = GameObject.Instantiate(panelobj, rootlayer, false);

        //获取对应UI组件返回
        panel = panelobj.GetComponent<BasePanel>();
        panel.ShowPanel();

        Debug.Log("新面板！");
        callBack?.Invoke(panel as T);
        //存储panel
        if (!panelDic.ContainsKey(panelName))
        {
            panelDic.Add(panelName, panel);
        }

        //经本人测试采用异步加载会导致相同逻辑在两帧内重复执行，故舍弃下面的异步加载方案
        ////不存在面板 加载面板
        //ABManager.Instance.LoadResAsync<GameObject>("ui", panelName, (res) =>
        //{
        //   //层级的处理
        //   Transform rootlayer = GetRootLayer(layer);
        //   //获取UI组件返回出去
        //   if (rootlayer == null)
        //            rootlayer = middleLayer;
        //   //将面板预制件创建到对应父layer下，并保持原本的缩放大小
        //   GameObject panelobj = GameObject.Instantiate(res, rootlayer, false);

        //    //获取对应UI组件返回
        //    T panel = panelobj.GetComponent<T>();
        //    panel.ShowPanel();
        //    Debug.Log(2);
        //    callBack?.Invoke(panel);

        //   //存储panel
        //   if(!panelDic.ContainsKey(panelName))
        //      panelDic.Add(panelName, panel);

        //    }, paltformType);

    }

   /// <summary>
   /// 隐藏面板
   /// </summary>
   /// <typeparam name="T">面板类型</typeparam>
   /// <param name="isDestroy">是否销毁面板（默认仅失活）</param>
    public void HidePanel<T>(bool isDestroy=false)
    {
        string panelName=typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
         panelDic[panelName].HidePanel();
           
            if (isDestroy) 
            {
                //选择销毁时才会移出字典
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
            else //仅失活
            {
                panelDic[panelName].gameObject.SetActive(false);
            }
        
        }

    }
    /// <summary>
    /// 获取面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GePanel<T>() where T : BasePanel
    {
        string panelName= typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
         return (T)panelDic[panelName];
        return null;
    }

    /// <summary>
    /// 为控件添加自定义事件
    /// </summary>
    /// <param name="control">对应控件</param>
    /// <param name="type">UI事件类型</param>
    /// <param name="callBack">相应的逻辑</param>
    public static void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        //保证一个控件上只会挂载一个eventtrigger
        if (trigger==null)
            trigger = control.gameObject.AddComponent<EventTrigger>();
            
        EventTrigger.Entry entry=new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    } 

}
