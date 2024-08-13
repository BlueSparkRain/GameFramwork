using System.Collections;

using UnityEngine;

using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SceneLoadManager:BaseSingletonManager<SceneLoadManager>
{
    private SceneLoadManager() {
    }
    
    //同步切换场景的方法
    public void LoadScene(string name, UnityAction callBack = null)
    {
        //切换场景
        SceneManager.LoadScene(name);
        //调用回调
        callBack?.Invoke();

    }
    
    //异步切换场景的方法
    public void LoadSceneAsync(string name, UnityAction callBack = null)
    {
        MonoManager .Instance.StartCoroutine(ReallyLoadSceneAsync(name, callBack));
    }
    
    private IEnumerator ReallyLoadSceneAsync(string name, UnityAction callBack)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);
        //不停的在协同程序中每帧检测是否加载结束 如果加载结束就不会进这个循环每帧执行了
        if (ao != null)
        {
            while (!ao.isDone)
            {
                //可以在这里利用事件中心 每一帧将进度发送给想要得到的地方
                EventCenter.Instance.EventTrigger<float>(E_EventType.E_sceneLoad, ao.progress);
                yield return 0;
            }
        }

        //避免最后一帧直接结束了 没有同步1出去
        EventCenter.Instance.EventTrigger<float>(E_EventType.E_sceneLoad, 1);
    
        callBack?.Invoke();
        callBack = null;
    }
}
