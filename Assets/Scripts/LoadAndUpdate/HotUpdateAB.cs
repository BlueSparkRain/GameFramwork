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
        //��һ��������ab�ļ�
        AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/new/test");
        //�ڶ�����������Դ
        Sprite sp = ab.LoadAsset<Sprite>("testphoto");
        image.sprite = sp;
        ab.Unload(false);//ע��ͬһ��ͬ��AssetBoundle�����ظ����أ�������ж�ص�ǰ�ģ��ټ����µ�

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


   
   /// ��������ص�ab������Դ����������ab���е���Դʱ(����������ϵ��ab������)
    public void DependenciesLoadAB() 
    {
        //������ab����AB��
        AssetBundle main = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/AB");
        //��ȡ��ab���������ļ���AB.manifest��
        AssetBundleManifest manifest = main.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        //����Ԥ�Ƽ����ڵ�����ab������������Щab��
        //deps�洢������������ab��������
        string[] deps = manifest.GetAllDependencies("newtest");

        //���ظ���ab������������ab��
        for (int i = 0; i < deps.Length; i++) 
        {
              AssetBundle.LoadFromFile(ConfigAB.ABPath + "/" + deps[i]);
        }

        //����Ԥ�Ƽ����ڵ�ab��������ab����
       AssetBundle newtest= AssetBundle.LoadFromFile(ConfigAB.ABPath + "/newtest");
        //���ظ�ab����Ԥ�Ƽ�
       GameObject prefab=  newtest.LoadAsset("prafab") as GameObject;
        Instantiate(prefab).transform.SetParent(GameObject.Find("Canvas").transform);
    }
}
