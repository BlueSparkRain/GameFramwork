using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Drawing.Printing;
public class ExportAB 
{

    [MenuItem("工具/AB包导出/Mac")]
    public static void ForMac() 
    {
        Export(BuildTarget.StandaloneOSX);
    }

    [MenuItem("工具/AB包导出/iOS")]
    public static void ForiOS() 
    {
        Export(BuildTarget.iOS);
    }

    [MenuItem("工具/AB包导出/Android")]
    public static void ForAndroid() 
    {
        Export(BuildTarget.Android);
    }

    [MenuItem("工具/AB包导出/Window")]
    public static void ForWindow()
    {
        Export(BuildTarget.StandaloneWindows);
    }

    static void Export(BuildTarget platform) 
    {
        
        string path = Application.dataPath;
       //截取至Assets文件夹，拼接上自己想要放入的文件夹名
        path = ConfigAB.ABPath+ platform;
      
        //防止路径不存在
        if (!Directory.Exists(path)) 
        {
        Directory.CreateDirectory(path);
        }
        //导出AB包核心代码，生成AB包文件  ab包文件存储路径，导出选项，平台（不同平台的ab包不同！）
        BuildPipeline.BuildAssetBundles(
            path,
            //注意：BuildAssetBundleOptions该类型枚举是按异或位运算，的多选枚举，以二进制来理解多选，十进制理解单选
            BuildAssetBundleOptions.ChunkBasedCompression|BuildAssetBundleOptions.ForceRebuildAssetBundle,// ab包将采用LZ4压缩格式 （保险）强制重新导出
            platform);//支持window平台
        Debug.Log(path);
        Debug.Log(platform+"导出AB包成功");
    }
}
