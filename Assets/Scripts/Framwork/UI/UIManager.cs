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
/// ��������UI���
/// ע�⣺Ԥ����������������豣��һ��
/// </summary>
public class UIManager : BaseSingletonManager<UIManager>
{
    Camera uiCamera;
    Canvas uiCanvas;
    EventSystem uiEventSystem;

    //�㼶������
    private Transform bottomLayer;
    private Transform middleLayer;
    private Transform topLayer;
    private Transform systemLayer;
    private UIManager()
    {
        //��̬����Ψһ��Canvas��Eventsystem��
        uiCamera = GameObject.Instantiate(Resources.Load("UI/UICamera")).GetComponent<Camera>();
        //UI��������Ƴ� 
        GameObject.DontDestroyOnLoad(uiCamera.gameObject);
        //��̬����Canvas
        uiCanvas = GameObject.Instantiate(Resources.Load("UI/Canvas")).GetComponent<Canvas>();
        //����UI�����
        uiCanvas.worldCamera = uiCamera;
        //���������Ƴ�
        GameObject.DontDestroyOnLoad(uiCanvas.gameObject);

        //�ҵ��㼶������
        bottomLayer = uiCanvas.transform.GetChild(0);
        middleLayer = uiCanvas.transform.GetChild(1);
        topLayer = uiCanvas.transform.GetChild(2);
        systemLayer = uiCanvas.transform.GetChild(3);

        //��̬����EventSystem
        uiEventSystem = GameObject.Instantiate(Resources.Load("UI/EventSystem")).GetComponent<EventSystem>();
        GameObject.DontDestroyOnLoad(uiEventSystem.gameObject);
    }

    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    /// <summary>
    /// ��ȡ��Ӧ�㼶��rootlayer������
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
    /// ��ʾ���
    /// ע�⣺������ͺ����Ԥ�������ֱ���һ��
    /// </summary>
    /// <typeparam name="T">�������ͣ����Ԥ�������ֱ���һ�£�</typeparam>
    /// <param name="panelName">��������</param>
    /// <param name="layer">�����ʾ�Ĳ㼶</param>
    /// <param name="callBack">���ڿ������첽���أ���ͨ��ί�лص���ʽ��������ɵ���崫�ݳ�ȥ����ʹ��</param>
    /// <param name="isSync"></param>
    public void ShowPanel<T>(E_ABPlatformType paltformType, E_UILayer layer = E_UILayer.Middle, UnityAction<T> callBack = null, bool isSync = false) where T : BasePanel
    {
        //��ȡ�������Ԥ����������������豣��һ��
        string panelName = typeof(T).Name;
        BasePanel panel;
        //�������
        if (panelDic.ContainsKey(panelName))
        {
            panel = panelDic[panelName];
            if (!panel.gameObject.activeSelf) 
              panel.gameObject.SetActive(true);

            panel.ShowPanel();
            Debug.Log("����Ѵ���");
            callBack?.Invoke(panelDic[panelName] as T);
            return;
        }
        //��������壬��ͨ��AB��������Դ
        GameObject panelobj = ABManager.Instance.LoadABRes<GameObject>("ui", panelName, paltformType);
        //�㼶�Ĵ���
        Transform rootlayer = GetRootLayer(layer);
        //��ȡUI������س�ȥ
        if (rootlayer == null)
            rootlayer = middleLayer;
        //�����Ԥ�Ƽ���������Ӧ��layer�£�������ԭ�������Ŵ�С
        panelobj = GameObject.Instantiate(panelobj, rootlayer, false);

        //��ȡ��ӦUI�������
        panel = panelobj.GetComponent<BasePanel>();
        panel.ShowPanel();

        Debug.Log("����壡");
        callBack?.Invoke(panel as T);
        //�洢panel
        if (!panelDic.ContainsKey(panelName))
        {
            panelDic.Add(panelName, panel);
        }

        //�����˲��Բ����첽���ػᵼ����ͬ�߼�����֡���ظ�ִ�У�������������첽���ط���
        ////��������� �������
        //ABManager.Instance.LoadResAsync<GameObject>("ui", panelName, (res) =>
        //{
        //   //�㼶�Ĵ���
        //   Transform rootlayer = GetRootLayer(layer);
        //   //��ȡUI������س�ȥ
        //   if (rootlayer == null)
        //            rootlayer = middleLayer;
        //   //�����Ԥ�Ƽ���������Ӧ��layer�£�������ԭ�������Ŵ�С
        //   GameObject panelobj = GameObject.Instantiate(res, rootlayer, false);

        //    //��ȡ��ӦUI�������
        //    T panel = panelobj.GetComponent<T>();
        //    panel.ShowPanel();
        //    Debug.Log(2);
        //    callBack?.Invoke(panel);

        //   //�洢panel
        //   if(!panelDic.ContainsKey(panelName))
        //      panelDic.Add(panelName, panel);

        //    }, paltformType);

    }

   /// <summary>
   /// �������
   /// </summary>
   /// <typeparam name="T">�������</typeparam>
   /// <param name="isDestroy">�Ƿ�������壨Ĭ�Ͻ�ʧ�</param>
    public void HidePanel<T>(bool isDestroy=false)
    {
        string panelName=typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
         panelDic[panelName].HidePanel();
           
            if (isDestroy) 
            {
                //ѡ������ʱ�Ż��Ƴ��ֵ�
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
            else //��ʧ��
            {
                panelDic[panelName].gameObject.SetActive(false);
            }
        
        }

    }
    /// <summary>
    /// ��ȡ���
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
    /// Ϊ�ؼ�����Զ����¼�
    /// </summary>
    /// <param name="control">��Ӧ�ؼ�</param>
    /// <param name="type">UI�¼�����</param>
    /// <param name="callBack">��Ӧ���߼�</param>
    public static void AddCustomEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        //��֤һ���ؼ���ֻ�����һ��eventtrigger
        if (trigger==null)
            trigger = control.gameObject.AddComponent<EventTrigger>();
            
        EventTrigger.Entry entry=new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    } 

}
