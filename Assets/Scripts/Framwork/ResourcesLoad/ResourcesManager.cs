using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ����첽���صĴ�������
/// </summary>
public class ResourcesManager : BaseSingletonManager<ResourcesManager>
{
    private ResourcesManager() { }
    
    

    /// <summary>
    /// ͨ�����ͽ����첽����
    /// </summary>
    /// <typeparam name="T">��Դ����</typeparam>
    /// <param name="path">Resources�µ���Դ·��</param>
    /// <param name="callBack">���ؽ�����Ļص����������첽������Դ������Ż����</param>
    public void LoadAsync<T>(string path,UnityAction<T> callBack) where T :UnityEngine.Object
    {
        //ͨ��Э���첽������Դ
        MonoManager.Instance.StartCoroutine(ReallyLoadAsync(path,callBack));
    }
    private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callBack) where T : UnityEngine.Object 
    {
        //�첽������Դ
        ResourceRequest rq=Resources.LoadAsync<T>(path);
        //�ȴ�����Դ���ؽ�����ִ�к���Ĵ���
        yield return rq;
        //��Դ���ؽ���������Դ�����ⲿ��ί�к���ȥ����ʹ��
        callBack(rq.asset as T);
    }


    /// <summary>
    /// ͨ��Type�����첽����
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <param name="callBack"></param>
    public void LoadAsync(string path, Type type, UnityAction<UnityEngine.Object> callBack) 
    {
        //ͨ��Э���첽������Դ
        MonoManager.Instance.StartCoroutine(ReallyLoadAsync(path,type, callBack));
    }
    private IEnumerator ReallyLoadAsync(string path, Type type, UnityAction<UnityEngine.Object> callBack) 
    {
        //�첽������Դ
        ResourceRequest rq = Resources.LoadAsync(path,type);
        //�ȴ�����Դ���ؽ�����ִ�к���Ĵ���
        yield return rq;
        //��Դ���ؽ���������Դ�����ⲿ��ί�к���ȥ����ʹ��
        callBack(rq.asset);
    }




    /// <summary>
    /// ָ��ж����Դ
    /// </summary>
    /// <param name="assetToUnload"></param>
    public void UnloadAsset(UnityEngine.Object assetToUnload) 
    {
    Resources.UnloadAsset(assetToUnload);
    
    }

    
    /// <summary>
    /// �첽ж�ض�Ӧû��ʹ�õ�Resources��ص���Դ
    /// </summary>
    /// <param name="callBack"></param>
    public void UnloadUnsedAssets(UnityAction callBack) 
    {
        MonoManager.Instance.StartCoroutine(RealUnloadUnsedAssets(callBack));
    
    }
    private IEnumerator RealUnloadUnsedAssets(UnityAction callBack) 
    {
        AsyncOperation ao=Resources.UnloadUnusedAssets();
        yield return ao;
        callBack();
    
    }
}
