using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class HotUpdateAB : MonoBehaviour
{
    public Image image;
    AssetBundle ab;

    void Start()
    {
       
      //  GameObject obj = ABManager.Instance.LoadABRes("modle", "Sphere", typeof(GameObject)) as GameObject;
      
    }

    /// <summary>
    /// 加载AB包
    /// </summary>
    public void LoadAB()
    {
        ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/old/test");
    }

    /// <summary>
    /// 加载并使用AB包中的资源
    /// </summary>
    public void LoadImage()
    {
        image.sprite = ab.LoadAsset<Sprite>("testphoto");
    }

    /// <summary>
    /// 卸载AB包
    /// </summary>
    /// <param name="deleteABRes">是否将场景中通过AB包加载出的资源，同AB包一起销毁</param>
    public void UnloadFile(bool deleteABRes)
    {
        //注意同一个同名AssetBoundle不能重复加载，必须先卸载当前的，再加载新的
        //如果不销毁资源，会出现内存方面的问题
        //通过profiler得到加载AB包文件不会使内存增加，加载资源时内存会增加
        //如果false，卸载包时资源不会销毁，当重复加载资源时，原先的资源被覆盖后成为游离的垃圾，产生的资源垃圾无法回收，导致内存会增加,需要使用Resources.UnloadUnusedAssets来清理垃圾
        //unity时揽回收机制，销毁物体也无法回收内存，需要手动回收！
        ab.Unload(false);
    }

    /// <summary>
    /// 手动清理游离的垃圾资源，降低内存
    /// </summary>
    public void Recycling()
    {
        //销毁未使用的垃圾资源
        Resources.UnloadUnusedAssets();
    }





    //导出的ab包一般分入不同的文件夹，可以实现新旧ab包中资源加载的切换
    //点击按钮加载新ab包里面的图
    public void ChangeAB()
    {
        //第一步，加载ab文件
        AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/new/test");
        //第二步，加载资源
        Sprite sp = ab.LoadAsset<Sprite>("testphoto");
        image.sprite = sp;
        ab.Unload(false);
    }



    /// 当所需加载的ab包中资源依赖于其他ab包中的资源时(存在依赖关系的ab包加载)
    public void DependenciesLoadAB()
    {
        //加载主ab包（AB）
        AssetBundle main = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/AB");
        //获取主ab包的配置文件（AB.manifest）
        AssetBundleManifest manifest = main.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        //分析预制件所在的新增ab包，依赖于哪些ab包
        //deps存储了所有依赖的ab包的名字
        string[] deps = manifest.GetAllDependencies("newtest");

        //加载该新ab包依赖的其他ab包
        for (int i = 0; i < deps.Length; i++)
        {
            AssetBundle.LoadFromFile(ConfigAB.ABPath + "/" + deps[i]);
        }

        //加载预制件所在的ab包（新增ab包）
        AssetBundle newtest = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/newtest");
        //加载该ab包内预制件
        GameObject prefab = newtest.LoadAsset("prafab") as GameObject;
        Instantiate(prefab).transform.SetParent(GameObject.Find("Canvas").transform);
    }


}
