using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using UnityEditor;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
/// <summary>
/// Editor资源管理器
/// //注意：只有在开发时能使用该管理器加载资源 用于开发功能
//发布后 是无法使用该管理器的，因为它需要用到编辑器相关功能
/// </summary>
public class EditorResManager : BaseSingletonManager<EditorResManager>
{
    //主要使用的API：AssetDatabase.LoadAssetAtPath   用于加载单个资源
    //AssetDatabase.LoadAllAssetRepresentationsAtPath 用于加载图集资源中的内容（加载所有子资源）
    //注意：两个API加载资源的路径都是从Assets/...开始
    //创建Editor文件夹，以后最终会打包为AB包的资源放在此处

    ////用于放置需要导入AB包的资源路径
    //private string rootPath = "Assets/Editor/ArtResources/";
    //private EditorResManager() { }


    ////1.加载单个资源
    //public T LoadEditorRes<T>(string path) where T : Object
    //{
    //    string suffixName = "";
    //    if (typeof(T) == typeof(GameObject)) 
    //    {
    //        suffixName = ".prefab";
    //    }
    //    T res = AssetDatabase.LoadAssetAtPath<T>(rootPath + path+suffixName);
    //    return res;
    //}

    ////2.加载图集相关资源
    //public Sprite LoadSprite(string path, string spriteName)
    //{
    //    Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
    //    //遍历所有子资源，得到同名图片返回
    //    foreach (Object obj in sprites)
    //    {

    //        if (spriteName == obj.name)
    //            return obj as Sprite;
    //    }
    //    return null;
    //}

    ///// <summary>
    ///// 加载图集文件的所有子图片并返回给外部
    ///// </summary>
    ///// <param name="path"></param>
    ///// <returns></returns>
    //public Dictionary<string, Sprite> LoadSprites(string path)
    //{
    //    Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
    //    Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(rootPath + path);
    //    foreach (Object obj in sprites)
    //    {
    //        spriteDic.Add(obj.name, obj as Sprite);
    //    }
    //    return spriteDic;
    //}
}
