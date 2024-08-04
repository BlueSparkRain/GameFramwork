using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class ExportAB 
{
    [MenuItem("工具/AB包导出")]
     static void Export() 
    {
        string path = Application.dataPath;
        path=path.Substring(0, path.Length-6)+"AB";//截取至Assets文件夹，拼接上自己想要放入的文件夹名

        //防止路径不存在
        if (Directory.Exists(path)) 
        {
        Directory.CreateDirectory(path);
        }
        //导出AB包核心代码，生成AB包文件  ab包文件存储路径，导出选项，平台（不同平台的ab包不同！）
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        Debug.Log("导出AB包成功");
    }
}
