using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour
{
    public Image ResAsyncLoadImage;
    public Image ABAsyncLoadImage;


    void Start()
    {
        ResAsyncLoadImage.sprite = Resources.Load("textphoto") as Sprite;
      //  StartCoroutine(AsyncLoadResources());
       // StartCoroutine(AsyncLoadAB());
    }

    /// <summary>
    /// 通过开启协同，执行一个Resources异步加载目录下资源
    /// </summary>
    /// <returns></returns>
    IEnumerator AsyncLoadResources() 
    {
        //开启异步加载
        ResourceRequest rr = Resources.LoadAsync<Sprite>("textphoto");
        //等待加载完成继续执行
        yield return rr;
        //显示资源
        ResAsyncLoadImage.sprite=rr.asset as Sprite ;
    
    }
    IEnumerator AsyncLoadAB()
    {
        AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(ConfigAB.ABPath + "/old/test");
        yield return abcr;
        ABAsyncLoadImage.sprite = abcr.assetBundle.LoadAsset< Sprite>("textphoto");
        

    }
}
