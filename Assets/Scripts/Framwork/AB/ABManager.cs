using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 让外部方柏霓进行资源加载
/// </summary>
public class ABManager : MonoSingleton<ABManager>
{
    private Dictionary<string,AssetBundle> abDic = new Dictionary<string,AssetBundle>();

    private AssetBundle mainAB=null;
    private AssetBundleManifest manifest=null;
   /// <summary>
   /// 主包名便于修改
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
    /// 加载AB包
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName) 
    {
        //先加载依赖包
        if (manifest == null)
        {
            //加载主ab包（AB）
            mainAB = AssetBundle.LoadFromFile(ConfigAB.ABPath + MainABName);

            //获取主ab包的配置文件（AB.manifest）
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        //分析预制件所在的新增ab包，依赖于哪些ab包
        //deps存储了所有依赖的ab包的名字
        string[] deps = manifest.GetAllDependencies(abName);

        //加载该新ab包依赖的其他ab包
        for (int i = 0; i < deps.Length; i++)
        {
            //判断依赖ab包是否已加载过
            if (!abDic.ContainsKey(deps[i]))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + deps[i]);
                abDic.Add(deps[i], ab);
            }
        }
        //加载预制件所在的ab包（新增ab包）
        AssetBundle newAB;
        if (!abDic.ContainsKey(abName))
        {
            newAB = AssetBundle.LoadFromFile(ConfigAB.ABPath + abName);
            abDic.Add(abName, newAB);
        }
    }

    //同步加载

    /// <summary>
    /// 同步加载1【根据名字同步加载】
    /// </summary>
    /// <param name="abName">ab包名称</param>
    /// <param name="resName">加载资源名称</param>
    public Object LoadABRes(string abName,string resName)
    {
        //加载AB包
        LoadAB(abName);
        //加载该ab包内资源
        //判断资源是否是Gameobject，如果是直接返回预制体实例化
        Object obj=abDic[abName].LoadAsset(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return  obj;
       
    }

    /// <summary>
    /// 同步加载2【通过Type指定资源类型】(避免同名不同类型资源的存在二义性)
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Object LoadABRes(string abName, string resName,System.Type type) 
    {
        //加载AB包
        LoadAB(abName);
        //加载该ab包内资源
        //判断资源是否是Gameobject，如果是直接返回预制体实例化
        Object obj = abDic[abName].LoadAsset(resName,type);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;

    }

    /// <summary>
    /// 同步加载3【泛型指定类型】省去了as 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public T LoadABRes<T>(string abName, string resName) where T :Object
    {
        //加载AB包
        LoadAB(abName);
        //加载该ab包内资源
        //判断资源是否是Gameobject，如果是直接返回预制体实例化
        T obj = abDic[abName].LoadAsset<T>(resName);
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //异步加载（）

    /// <summary>
    /// 异步加载1【根据名字异步加载资源，提供给外部，在此处启动携程】
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
        //加载AB包
        LoadAB(abName);
        //加载该ab包内资源
        //判断资源是否是Gameobject，如果是直接返回预制体实例化
        AssetBundleRequest abr=  abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //加载完成后通过委托传递给外部来使用
        if (abr.asset is GameObject)
          callBack( Instantiate(abr.asset));
        else
            callBack(abr.asset);
           
    }
   
  
    /// <summary>
    /// 异步加载2【根据Type进行异步加载指定类型的资源】
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
        //加载AB包
        LoadAB(abName);
        //加载该ab包内资源
        //判断资源是否是Gameobject，如果是直接返回预制体实例化
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        //加载完成后通过委托传递给外部来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);

    }

   
    /// <summary>
    /// 异步加载3【根据泛型加载】
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
        //加载AB包
        LoadAB(abName);
        //加载该ab包内资源
        //判断资源是否是Gameobject，如果是直接返回预制体实例化
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //加载完成后通过委托传递给外部来使用
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset) as T) ;
        else
            callBack(abr.asset as T);
    }




    //单个包卸载
    public void Unload(string abName) 
    {
    if (abDic.ContainsKey(abName)) 
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
        Recycling();
    }

    //所有包卸载
    public void ClearAB() 
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        Recycling();
        mainAB = null;
        manifest = null;
    }

    //释放游离垃圾资源
    void Recycling() 
    {
        Resources.UnloadUnusedAssets();
    }

}








