using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// Editor��Դ������
/// //ע�⣺ֻ���ڿ���ʱ��ʹ�øù�����������Դ ���ڿ�������
//������ ���޷�ʹ�øù������ģ���Ϊ����Ҫ�õ��༭����ع���
/// </summary>
public class EditorResManager : BaseSingletonManager<EditorResManager>
{
    //��Ҫʹ�õ�API��AssetDatabase.LoadAssetAtPath ���ڼ��ص�����Դ
    //AssetDatabase.LoadAllAssetRepresentationsAtPath ���ڼ���ͼ����Դ�е����ݣ�������������Դ��
    //ע�⣺����API������Դ��·�����Ǵ�Assets/...��ʼ
    //����Editor�ļ��У��Ժ����ջ���ΪAB������Դ���ڴ˴�

    //���ڷ�����Ҫ����AB������Դ·��
   // private string rootPath = "Assets/Editor/ArtResources/";
    private EditorResManager() { }


    //    //1.���ص�����Դ
    //    public T LoadEditorRes<T>(string path) where T : Object
    //    {
    //#if UNITY_EDITOR
    //        string suffixName = "";
    //        if (typeof(T) == typeof(GameObject))
    //        {
    //            suffixName = ".prefab";
    //        }
    //        T res = AssetDatabase.LoadAssetAtPath<T>(rootPath + path + suffixName);
    //        return res;
    //#else
    //   return null
    //#endif
    //    }

    //    //2.����ͼ�������Դ
    //    public Sprite LoadSprite(string path, string spriteName)
    //    {
    //#if UNITY_EDITOR
    //        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
    //        //������������Դ���õ�ͬ��ͼƬ����
    //        foreach (Object obj in sprites)
    //        {

    //            if (spriteName == obj.name)
    //                return obj as Sprite;
    //        }
    //        return null;
    //#else
    //   return null
    //#endif
    //    }

    //    /// <summary>
    //    /// ����ͼ���ļ���������ͼƬ�����ظ��ⲿ
    //    /// </summary>
    //    /// <param name="path"></param>
    //    /// <returns></returns>
    //    public Dictionary<string, Sprite> LoadSprites(string path)
    //    {
    //#if UNITY_EDITOR
    //        Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
    //        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
    //        foreach (Object obj in sprites)
    //        {
    //            spriteDic.Add(obj.name, obj as Sprite);
    //        }
    //        return spriteDic;
    //#else
    //        return null
    //#endif
    //    }
}
