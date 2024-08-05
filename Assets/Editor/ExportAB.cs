using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class ExportAB 
{

    [MenuItem("����/AB������/Mac")]
    public static void ForMac() 
    {
        Export(BuildTarget.StandaloneOSX);
    }

    [MenuItem("����/AB������/iOS")]
    public static void ForiOS() 
    {
        Export(BuildTarget.iOS);
    }

    [MenuItem("����/AB������/Android")]
    public static void ForAndroid() 
    {
        Export(BuildTarget.Android);
    }

    [MenuItem("����/AB������/Window")]
    public static void ForWindow()
    {
        Export(BuildTarget.StandaloneWindows);
    }

    static void Export(BuildTarget platform) 
    {
        string path = Application.dataPath;
        path=path.Substring(0, path.Length-6)+"AB";//��ȡ��Assets�ļ��У�ƴ�����Լ���Ҫ������ļ�����

        //��ֹ·��������
        if (Directory.Exists(path)) 
        {
        Directory.CreateDirectory(path);
        }
        //����AB�����Ĵ��룬����AB���ļ�  ab���ļ��洢·��������ѡ�ƽ̨����ͬƽ̨��ab����ͬ����
        BuildPipeline.BuildAssetBundles(
            path,
            //ע�⣺BuildAssetBundleOptions������ö���ǰ����λ���㣬�Ķ�ѡö�٣��Զ�����������ѡ��ʮ������ⵥѡ
            BuildAssetBundleOptions.ChunkBasedCompression|BuildAssetBundleOptions.ForceRebuildAssetBundle,// ab��������LZ4ѹ����ʽ �����գ�ǿ�����µ���
            platform);//֧��windowƽ̨
        Debug.Log(platform+"����AB���ɹ�");
    }
}
