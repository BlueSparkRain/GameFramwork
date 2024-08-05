using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class HotUpdateAB : MonoBehaviour
{
   public Image image;
    void Start()
    {
        //第一步，加载ab文件
        AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/new/test");
        //第二步，加载资源
        Sprite sp = ab.LoadAsset<Sprite>("testphoto");
        image.sprite = sp;
        ab.Unload(false);//注意同一个同名AssetBoundle不能重复加载，必须先卸载当前的，再加载新的

    }

   //导出的ab包一般分入不同的文件夹，可以实现新旧ab包中资源加载的切换
   //点击按钮加载新ab包里面的图
    public void ChangeAB()
    {
        //第一步，加载ab文件
        AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/old/test");
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
       AssetBundle newtest= AssetBundle.LoadFromFile(ConfigAB.ABPath + "/newtest");
        //加载该ab包内预制件
       GameObject prefab=  newtest.LoadAsset("prafab") as GameObject;
        Instantiate(prefab).transform.SetParent(GameObject.Find("Canvas").transform);
    }
}
