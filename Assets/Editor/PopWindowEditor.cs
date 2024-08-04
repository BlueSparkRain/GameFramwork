using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// 一般的插件就是通过将监视器拓展应用在窗口内部实现的
/// </summary>
public class PopWindowEditor : EditorWindow
{
    [MenuItem("工具/创建窗口")]
  static void OpenWindow() 
    {
      PopWindowEditor window= GetWindow<PopWindowEditor>(false, "弹窗标题", true);
      window.minSize = new Vector2(100, 200);
        
    }
    //开启窗口调用
    private void OnEnable()
    {
        
    }
    //关闭窗口调用
    private void OnDisable()
    {
        

    }
    //窗口开启期间每帧调用
    private void Update()
    {
        

    }
    private void OnGUI()
    {
        if (GUILayout.Button("窗口按钮测试")) 
        {
            Debug.Log("window按钮点击");
        }
    }
    //场景change时调用
    private void OnHierarchyChange()
    {
    }
    //选中游戏物体change时调用（Selection有多种成员方法可以获取当前选中物体的的Transform和gamobject）
    private void OnSelectionChange()
    {
        Debug.Log(Selection.activeGameObject.name);
    }
}
