using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/// <summary>
/// һ��Ĳ������ͨ������������չӦ���ڴ����ڲ�ʵ�ֵ�
/// </summary>
public class PopWindowEditor : EditorWindow
{
    [MenuItem("����/��������")]
  static void OpenWindow() 
    {
      PopWindowEditor window= GetWindow<PopWindowEditor>(false, "��������", true);
      window.minSize = new Vector2(100, 200);
        
    }
    //�������ڵ���
    private void OnEnable()
    {
        
    }
    //�رմ��ڵ���
    private void OnDisable()
    {
        

    }
    //���ڿ����ڼ�ÿ֡����
    private void Update()
    {
        

    }
    private void OnGUI()
    {
        if (GUILayout.Button("���ڰ�ť����")) 
        {
            Debug.Log("window��ť���");
        }
    }
    //����changeʱ����
    private void OnHierarchyChange()
    {
    }
    //ѡ����Ϸ����changeʱ���ã�Selection�ж��ֳ�Ա�������Ի�ȡ��ǰѡ������ĵ�Transform��gamobject��
    private void OnSelectionChange()
    {
        Debug.Log(Selection.activeGameObject.name);
    }
}
