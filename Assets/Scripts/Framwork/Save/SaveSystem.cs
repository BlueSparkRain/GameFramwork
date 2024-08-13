using System;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
   //将可以被序列化的数据结构转换为json字符串文件存储于特殊文件夹persistentData内
    public static void SaveByJson(string fileName,object data)
    {
     string path=Path.Combine(Application.persistentDataPath,fileName);
     string json = JsonUtility.ToJson(data);
    
     try
     {
         if(json!=null) File.WriteAllText(path,json);
         #if UNITY_EDITOR
         Debug.Log($"成功于路径：{path}储存文件:{fileName}");
         #endif
     }
     catch (Exception e)
     {    
         Console.WriteLine(e); 
     }
    }
    //将特定文件名称路径下存储的文件的json字符串反序列化为对应的数据结构并返回
    public static T LoadFromJson<T>(string fileName)
    {
        string path=Path.Combine(Application.persistentDataPath,fileName);
       
        try
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            #if UNITY_EDITOR
            Debug.Log($"成功读取文件:{fileName}");
            #endif
            return data;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    //删除已存储的对应名称的json文件
    public static void DeleteSavedFile(string fileName)
    {
        string path=Path.Combine(Application.persistentDataPath,fileName);
        try
        {
          File.Delete(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    
}

public enum JsonFileName
{
    PlayerPos,
    PlayerHP,
    PlayerSp,
    PlayerScore,
    
}