using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ���ⲿ�����޽�����Դ����
/// </summary>
public class ABManager : MonoSingleton<ABManager>
{
    private Dictionary<string,AssetBundle> abDic = new Dictionary<string,AssetBundle>();

    private AssetBundle mainAB=null;
    private AssetBundleManifest manifest=null;
   /// <summary>
   /// �����������޸�
   /// </summary>
    private string MainABName 
    {
        get {
#if UNITY_IOS
            return "IOS";
#elif UNITY_ANDROID
            return "Android";
#else 
            return "PC";
#endif
        } 
    }

    /// <summary>
    /// ����AB��
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName) 
    {
        //�ȼ���������
        if (manifest == null)
        {
            //������ab����AB��
            mainAB = AssetBundle.LoadFromFile(ConfigAB.ABPath + MainABName);

            //��ȡ��ab���������ļ���AB.manifest��
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        //����Ԥ�Ƽ����ڵ�����ab������������Щab��
        //deps�洢������������ab��������
        string[] deps = manifest.GetAllDependencies(abName);

        //���ظ���ab������������ab��
        for (int i = 0; i < deps.Length; i++)
        {
            //�ж�����ab���Ƿ��Ѽ��ع�
            if (!abDic.ContainsKey(deps[i]))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + deps[i]);
                abDic.Add(deps[i], ab);
            }
        }
        //����Ԥ�Ƽ����ڵ�ab��������ab����
        AssetBundle newAB;
        if (!abDic.ContainsKey(abName))
        {
            newAB = AssetBundle.LoadFromFile(ConfigAB.ABPath + abName);
            abDic.Add(abName, newAB);
        }
    }

    //ͬ������

    /// <summary>
    /// ͬ������1����������ͬ�����ء�
    /// </summary>
    /// <param name="abName">ab������</param>
    /// <param name="resName">������Դ����</param>
    public Object LoadABRes(string abName,string resName)
    {
        //����AB��
        LoadAB(abName);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        Object obj=abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return  obj;
       
    }

    /// <summary>
    /// ͬ������2��ͨ��Typeָ����Դ���͡�(����ͬ����ͬ������Դ�Ĵ��ڶ�����)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadABRes(string abName, string resName,System.Type type) 
    {
        //����AB��
        LoadAB(abName);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;

    }

    /// <summary>
    /// ͬ������3������ָ�����͡�ʡȥ��as 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadABRes<T>(string abName, string resName) where T :Object
    {
        //����AB��
        LoadAB(abName);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //�첽���أ���

    /// <summary>
    /// �첽����1�����������첽������Դ���ṩ���ⲿ���ڴ˴�����Я�̡�
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync(string abName,string resName,UnityAction<object> callBack) 
    {
        StartCoroutine(ReallyLoadResAsync( abName,  resName,  callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<object> callBack)
    {
        //����AB��
        LoadAB(abName);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        AssetBundleRequest abr=  abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //������ɺ�ͨ��ί�д��ݸ��ⲿ��ʹ��
        if (abr.asset is GameObject)
          callBack( Instantiate(abr.asset));
        else
            callBack(abr.asset);
           
    }
   
  
    /// <summary>
    /// �첽����2������Type�����첽����ָ�����͵���Դ��
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync(string abName, string resName,System.Type type, UnityAction<object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName,System.Type type, UnityAction<object> callBack)
    {
        //����AB��
        LoadAB(abName);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        //������ɺ�ͨ��ί�д��ݸ��ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);

    }

   
    /// <summary>
    /// �첽����3�����ݷ��ͼ��ء�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
     }
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T:Object
    {
        //����AB��
        LoadAB(abName);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //������ɺ�ͨ��ί�д��ݸ��ⲿ��ʹ��
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset) as T) ;
        else
            callBack(abr.asset as T);
    }




    //������ж��
    public void Unload(string abName) 
    {
    if (abDic.ContainsKey(abName)) 
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
        Recycling();
    }

    //���а�ж��
    public void ClearAB() 
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        Recycling();
        mainAB = null;
        manifest = null;
    }

    //�ͷ�����������Դ
    void Recycling() 
    {
        Resources.UnloadUnusedAssets();
    }

}








