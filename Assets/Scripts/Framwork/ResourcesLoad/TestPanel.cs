using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestPanel : BasePanel
{
    public override void HidePanel()
    {
        Debug.Log("CloseMe���ر����ʱִ���߼�");
    }

    public override void ShowPanel()
    {
        Debug.Log("ShowMe����ʾ���ʱִ���߼�");
    }

    public void TestFun() 
    {
        Debug.Log("��������ڷ���ִ���߼�");
    }
   
}
