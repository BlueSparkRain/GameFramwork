using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// AB包根目录配置
/// </summary>
public class ConfigAB : MonoBehaviour
{
    public static string ABPath = Application.dataPath.Substring(0, Application.dataPath.Length - 6) + "AssetBundle/";
    /// <summary>
    /// 为了便于获取不同平台下的主AB包
    /// </summary>
    public static string AB_iOS_List = "iOS/";
    public static string AB_Android_List = "Android/";
    public static string AB_Mac_List = "StandaloneOSX/";
    public static string AB_StandaloneWindows_List = "StandaloneWindows/";
}
