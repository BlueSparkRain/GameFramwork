using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotUpdateAB : MonoBehaviour
{
   public Image image;
    void Start()
    {
        //��һ��������ab�ļ�
        AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/new/test");
        //�ڶ�����������Դ
        Sprite sp = ab.LoadAsset<Sprite>("testphoto");
        image.sprite = sp;
        ab.Unload(false);

    }
    //������ab��һ����벻ͬ���ļ��У�����ʵ���¾�ab������Դ���ص��л�
   //�����ť������ab�������ͼ
    public void ChangeAB()
    {
        //��һ��������ab�ļ�
        AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/old/test");
        //�ڶ�����������Դ
        Sprite sp = ab.LoadAsset<Sprite>("testphoto");
        image.sprite = sp;
        ab.Unload(false);
    }
   
}
