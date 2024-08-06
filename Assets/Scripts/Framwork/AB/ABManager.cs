using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum E_ABPlatformType
{
    iOS, Android, Mac, Window
}

/// <summary>
/// ���ⲿ���������Դ����
/// </summary>
public class ABManager : MonoSingleton<ABManager>
{
    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    private AssetBundle mainAB = null;
    private AssetBundleManifest manifest = null;

    string GetPlatformList(E_ABPlatformType platform)
    {
        switch (platform)
        {
            case E_ABPlatformType.iOS:
                return ConfigAB.AB_iOS_List;
            case E_ABPlatformType.Android:
                return ConfigAB.AB_Android_List;
            case E_ABPlatformType.Mac:
                return ConfigAB.AB_Mac_List;
            case E_ABPlatformType.Window:
                return ConfigAB.AB_StandaloneWindows_List;
            default:
                return ConfigAB.AB_StandaloneWindows_List;
        }
    }
    /// <summary>
    /// ���ݰ�������AB��
    /// </summary>
    /// <param name="abName">ab������</param>
    /// <param name="platformType">·��ƽ̨�ļ���ǰ׺</param>
    public void LoadAB(string abName, E_ABPlatformType platformType)
    {
        string abPlatformPath = GetPlatformList(platformType);

        //�ȼ���������
        if (manifest == null)
        {
            //������ab����AB��
            mainAB = AssetBundle.LoadFromFile(ConfigAB.ABPath + abPlatformPath + abPlatformPath.Substring(0, abPlatformPath.Length - 1));

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
            newAB = AssetBundle.LoadFromFile(ConfigAB.ABPath + abPlatformPath + abName);
            abDic.Add(abName, newAB);
        }
    }

    //ͬ������

    /// <summary>
    /// ͬ������1����������ͬ�����ء�
    /// </summary>
    /// <param name="abName">ab������</param>
    /// <param name="resName">������Դ����</param>
    public Object LoadABRes(string abName, string resName, E_ABPlatformType platformType)
    {
        //����AB��
        LoadAB(abName, platformType);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        Object obj = abDic[abName].LoadAsset(resName);
        return obj;
    }

    /// <summary>
    /// ͬ������2��ͨ��Typeָ����Դ���͡�(����ͬ����ͬ������Դ�Ĵ��ڶ�����)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadABRes(string abName, string resName, System.Type type, E_ABPlatformType platformType)
    {
        //����AB��
        LoadAB(abName, platformType);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        Object obj = abDic[abName].LoadAsset(resName, type);
        return obj;
    }

    /// <summary>
    /// ͬ������3������ָ�����͡�ʡȥ��as 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadABRes<T>(string abName, string resName, E_ABPlatformType platformType) where T : Object
    {
        //����AB��
        LoadAB(abName, platformType);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        T obj = abDic[abName].LoadAsset<T>(resName);
        return obj;
    }

    //�첽���أ���

    /// <summary>
    /// �첽����1�����������첽������Դ���ṩ���ⲿ���ڴ˴�����Я�̡�
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync(string abName, string resName, UnityAction<object> callBack, E_ABPlatformType platformType)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack, platformType));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<object> callBack, E_ABPlatformType platformType)
    {
        //����AB��
        LoadAB(abName, platformType);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //������ɺ�ͨ��ί�д��ݸ��ⲿ��ʹ��
        callBack(abr.asset);
    }


    /// <summary>
    /// �첽����2������Type�����첽����ָ�����͵���Դ��
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<object> callBack, E_ABPlatformType platformType)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack, platformType));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<object> callBack, E_ABPlatformType platformType)
    {
        //����AB��
        LoadAB(abName, platformType);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);
        yield return abr;
        //������ɺ�ͨ��ί�д��ݸ��ⲿ��ʹ��
         callBack(abr.asset);
    }


    /// <summary>
    /// �첽����3�����ݷ��ͼ��ء�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="callBack"></param>
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack, E_ABPlatformType platformType) where T : Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack, platformType));
    }
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack, E_ABPlatformType platformType) where T : Object
    {
        //����AB��
        LoadAB(abName, platformType);
        //���ظ�ab������Դ
        //�ж���Դ�Ƿ���Gameobject�������ֱ�ӷ���Ԥ����ʵ����
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //������ɺ�ͨ��ί�д��ݸ��ⲿ��ʹ��
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








