using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPanel : BasePanel
{
    public override void HidePanel()
    {
        Debug.Log("CloseMe：关闭面板时执行逻辑");
    }

    public override void ShowPanel()
    {
        Debug.Log("ShowMe：显示面板时执行逻辑");
    }

    public void TestFun() 
    {
        Debug.Log("测试面板内方法执行逻辑");
    }
   
}
