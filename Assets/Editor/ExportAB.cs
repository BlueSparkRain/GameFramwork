using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class ExportAB 
{
    [MenuItem("����/AB������")]
     static void Export() 
    {
        string path = Application.dataPath;
        path=path.Substring(0, path.Length-6)+"AB";//��ȡ��Assets�ļ��У�ƴ�����Լ���Ҫ������ļ�����

        //��ֹ·��������
        if (Directory.Exists(path)) 
        {
        Directory.CreateDirectory(path);
        }
        //����AB�����Ĵ��룬����AB���ļ�  ab���ļ��洢·��������ѡ�ƽ̨����ͬƽ̨��ab����ͬ����
        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        Debug.Log("����AB���ɹ�");
    }
}
