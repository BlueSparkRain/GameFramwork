using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 解决异步加载的代码冗余
/// </summary>
public class ResourcesManager : BaseSingletonManager<ResourcesManager>
{
    private ResourcesManager() { }
    
    

    /// <summary>
    /// 通过泛型进行异步加载
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="path">Resources下的资源路径</param>
    /// <param name="callBack">加载结束后的回调函数，当异步加载资源结束后才会调用</param>
    public void LoadAsync<T>(string path,UnityAction<T> callBack) where T :UnityEngine.Object
    {
        //通过协程异步加载资源
        MonoManager.Instance.StartCoroutine(ReallyLoadAsync(path,callBack));
    }
    private IEnumerator ReallyLoadAsync<T>(string path, UnityAction<T> callBack) where T : UnityEngine.Object 
    {
        //异步加载资源
        ResourceRequest rq=Resources.LoadAsync<T>(path);
        //等待中资源加载结束，执行后面的代码
        yield return rq;
        //资源加载结束，将资源传到外部的委托函数去进行使用
        callBack(rq.asset as T);
    }


    /// <summary>
    /// 通过Type进行异步加载
    /// </summary>
    /// <param name="path"></param>
    /// <param name="type"></param>
    /// <param name="callBack"></param>
    public void LoadAsync(string path, Type type, UnityAction<UnityEngine.Object> callBack) 
    {
        //通过协程异步加载资源
        MonoManager.Instance.StartCoroutine(ReallyLoadAsync(path,type, callBack));
    }
    private IEnumerator ReallyLoadAsync(string path, Type type, UnityAction<UnityEngine.Object> callBack) 
    {
        //异步加载资源
        ResourceRequest rq = Resources.LoadAsync(path,type);
        //等待中资源加载结束，执行后面的代码
        yield return rq;
        //资源加载结束，将资源传到外部的委托函数去进行使用
        callBack(rq.asset);
    }




    /// <summary>
    /// 指定卸载资源
    /// </summary>
    /// <param name="assetToUnload"></param>
    public void UnloadAsset(UnityEngine.Object assetToUnload) 
    {
    Resources.UnloadAsset(assetToUnload);
    
    }

    
    /// <summary>
    /// 异步卸载对应没有使用的Resources相关的资源
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
