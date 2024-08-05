using System.Collections;
using System.Collections.Generic;
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
        ab.Unload(false);

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
   
}
