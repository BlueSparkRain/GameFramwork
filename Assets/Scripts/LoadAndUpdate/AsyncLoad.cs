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
       // ResAsyncLoadImage.sprite = Resources.Load("textphoto") as Sprite;
        StartCoroutine(AsyncLoadResources());
        StartCoroutine(AsyncLoadAB());
    }

    /// <summary>
    /// ͨ������Эͬ��ִ��һ��Resources�첽����Ŀ¼����Դ
    /// </summary>
    /// <returns></returns>
    IEnumerator AsyncLoadResources() 
    {
        //�����첽����
        ResourceRequest rr = Resources.LoadAsync<Sprite>("Sprites/testphoto");
        //�ȴ�������ɼ���ִ��
        yield return rr;
        Debug.Log("��ʾ�첽����");
        //��ʾ��Դ
        
        ResAsyncLoadImage.sprite=rr.asset as Sprite ;
    
    }
    IEnumerator AsyncLoadAB()
    {
        AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(ConfigAB.ABPath + "/new/test");
        //�ȴ��첽���س�ab��
        yield return abcr;
        //ʹ��ab���е���Դ
        AssetBundleRequest rr = abcr.assetBundle.LoadAssetAsync<Sprite>("testphoto");
        yield return rr;

        ABAsyncLoadImage.sprite=rr.asset as Sprite;
    }
}
