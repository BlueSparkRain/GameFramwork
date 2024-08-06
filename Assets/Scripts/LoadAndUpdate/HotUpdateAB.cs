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
    /// ����AB��
    /// </summary>
    public void LoadAB()
    {
        ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/old/test");
    }

    /// <summary>
    /// ���ز�ʹ��AB���е���Դ
    /// </summary>
    public void LoadImage()
    {
        image.sprite = ab.LoadAsset<Sprite>("testphoto");
    }

    /// <summary>
    /// ж��AB��
    /// </summary>
    /// <param name="deleteABRes">�Ƿ񽫳�����ͨ��AB�����س�����Դ��ͬAB��һ������</param>
    public void UnloadFile(bool deleteABRes)
    {
        //ע��ͬһ��ͬ��AssetBoundle�����ظ����أ�������ж�ص�ǰ�ģ��ټ����µ�
        //�����������Դ��������ڴ淽�������
        //ͨ��profiler�õ�����AB���ļ�����ʹ�ڴ����ӣ�������Դʱ�ڴ������
        //���false��ж�ذ�ʱ��Դ�������٣����ظ�������Դʱ��ԭ�ȵ���Դ�����Ǻ��Ϊ�������������������Դ�����޷����գ������ڴ������,��Ҫʹ��Resources.UnloadUnusedAssets����������
        //unityʱ�����ջ��ƣ���������Ҳ�޷������ڴ棬��Ҫ�ֶ����գ�
        ab.Unload(false);
    }

    /// <summary>
    /// �ֶ����������������Դ�������ڴ�
    /// </summary>
    public void Recycling()
    {
        //����δʹ�õ�������Դ
        Resources.UnloadUnusedAssets();
    }





    //������ab��һ����벻ͬ���ļ��У�����ʵ���¾�ab������Դ���ص��л�
    //�����ť������ab�������ͼ
    public void ChangeAB()
    {
        //��һ��������ab�ļ�
        AssetBundle ab = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/new/test");
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
        AssetBundle newtest = AssetBundle.LoadFromFile(ConfigAB.ABPath + "/newtest");
        //���ظ�ab����Ԥ�Ƽ�
        GameObject prefab = newtest.LoadAsset("prafab") as GameObject;
        Instantiate(prefab).transform.SetParent(GameObject.Find("Canvas").transform);
    }


}
