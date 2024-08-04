using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class NodeWindow:EditorWindow
{
    static NodeWindow window;
    static GameObject nodesManager;

    public static void OpenWindow(GameObject nodeManager) 
    {
        nodesManager = nodeManager;
        window = EditorWindow.GetWindow<NodeWindow>();
    }

    //ͨ����updateȷ����ǰ�༭�Ķ���һֱΪ���ڱ�ѡ�еĶ���
    private void Update()
    {
        Selection.activeGameObject = nodesManager;
    }
    public static void CloseWindow() 
    {
    window.Close();
    }

}
//���ʽ����NodeManager�ű�
[CustomEditor(typeof(NodeManager))]
public class NodeManagerEditor :Editor
{
    NodeManager nodesManager;
    bool isEditor;//�Ƿ�Ϊ�༭״̬

    //��ѡ�д���NodeManager�ű��Ķ���ʱ�����Ŀ�����
    private void OnEnable()
    {
        nodesManager=(NodeManager)target;
        Debug.Log("����Ŀ��");
    }

    //����������������ں���
    public override void OnInspectorGUI()
    {
        OtherDataDraw("nodes", "·��");
        if (!isEditor && GUILayout.Button("��ʼ�༭�ڵ�")) 
        {
            NodeWindow.OpenWindow(nodesManager.gameObject);//�򿪴���
            isEditor = true;//����༭״̬
        }
        else if (isEditor && GUILayout.Button("�����༭�ڵ�")) 
        {
            NodeWindow.CloseWindow();
            isEditor = false;
        }

        if (GUILayout.Button("ɾ�����һ���ڵ�"))
        {
        RemoveAtLastNode();
        }

        if (GUILayout.Button("ɾ�����нڵ�"))
        {
        RemoveAllNodes();
        }
    }
    /// <summary>
    /// �ռ��������ͻ���
    /// </summary>
    /// <param name="originalDataName"></param>
    /// <param name="processedHeadName"></param>
    void OtherDataDraw(string originalDataName, string processedHeadName)
    {
        //���¿����л�����
        serializedObject.Update();
        //ͨ����Ա�������ҵ�����ϵĳ�Ա����
        SerializedProperty sp = serializedObject.FindProperty(originalDataName);
        //�����л����ݻ��ƣ�ȡ�������ݣ����⣬�Ƿ����л�õ����л�������ʾ��
        EditorGUILayout.PropertyField(sp, new GUIContent(processedHeadName), true);
        //���޸ĵ�����д�뵽�����л���ԭʼ������
        serializedObject.ApplyModifiedProperties();

    }

  
    RaycastHit hit;

    //��ѡ�й����Ľű����ص����壬�����Scene��ͼ�·����仯ʱ������ƶ����ʱ�������ã�
    private void OnSceneGUI()
    {
        if (!isEditor)//�Ǳ༭״̬��ֱ�ӷ��� 
          return;

        //��갴�����ʱ�������ߣ�������ʱʹ��Event��
        //Event.current.button:�ж�������ĸ�����
        //Event.current.type:�ж������¼���ʽ
        if (Event.current.button == 0 && Event.current.type == EventType.MouseDown) 
        {
            //�����λ�÷�������
            //��Ϊ�Ǵ�Scene��ͼ�·������ߣ��������е��������û�й�ϵ�����Բ���ʹ������������ߵķ���
            //��GUI�е�һ���������綨��һ�����ߣ�����һ�㶼����������
     
            Ray ray=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if(Physics.Raycast(ray,out hit, 100, 1 << 31)) 
            {
               InstancePathNode(hit.point + Vector3.up * 0.1f);
            }
        }
    }
    
    /// <summary>
    /// �����½ڵ�
    /// </summary>
    /// <param name="targetPos"></param>
    void InstancePathNode(Vector3 targetPos) 
    {
       
        GameObject prefab = Resources.Load<GameObject>("Prefabs/changli");
        GameObject newPathNode=Instantiate(prefab,targetPos,Quaternion.identity,nodesManager.transform);
        nodesManager.nodes.Add(newPathNode);//�����ɵĽڵ�����б�
    }

    //ɾ�����һ���ڵ�
    void RemoveAtLastNode()
    {
        if (nodesManager.nodes.Count > 0) 
        {
            Debug.Log("ɾ��");
            //������ɾ��������һ���ڵ�����
            DestroyImmediate(nodesManager.nodes[nodesManager.nodes.Count - 1]);
           //���б����Ƴ�
             nodesManager.nodes.RemoveAt(nodesManager.nodes.Count - 1);
        }
    }
    //ɾ�����нڵ�
    void RemoveAllNodes()
    {
        for (int i = 0; i < nodesManager.nodes.Count; i++) 
        {
            DestroyImmediate(nodesManager.nodes[i]);
        }
        nodesManager.nodes.Clear();
    }
}
