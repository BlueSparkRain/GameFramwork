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
    /// ͨ������Эͬ��ִ��һ��Resources�첽����Ŀ¼����Դ
    /// </summary>
    /// <returns></returns>
    IEnumerator AsyncLoadResources() 
    {
        //�����첽����
        ResourceRequest rr = Resources.LoadAsync<Sprite>("textphoto");
        //�ȴ�������ɼ���ִ��
        yield return rr;
        //��ʾ��Դ
        ResAsyncLoadImage.sprite=rr.asset as Sprite ;
    
    }
    IEnumerator AsyncLoadAB()
    {
        AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(ConfigAB.ABPath + "/old/test");
        yield return abcr;
        ABAsyncLoadImage.sprite = abcr.assetBundle.LoadAsset< Sprite>("textphoto");
        

    }
}
