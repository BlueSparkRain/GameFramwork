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

    //通过该update确保当前编辑的对象一直为正在被选中的对象
    private void Update()
    {
        Selection.activeGameObject = nodesManager;
    }
    public static void CloseWindow() 
    {
    window.Close();
    }

}
//外挂式关联NodeManager脚本
[CustomEditor(typeof(NodeManager))]
public class NodeManagerEditor :Editor
{
    NodeManager nodesManager;
    bool isEditor;//是否为编辑状态

    //当选中带有NodeManager脚本的对象时，获得目标组件
    private void OnEnable()
    {
        nodesManager=(NodeManager)target;
        Debug.Log("发现目标");
    }

    //绘制组件的生命周期函数
    public override void OnInspectorGUI()
    {
        OtherDataDraw("nodes", "路径");
        if (!isEditor && GUILayout.Button("开始编辑节点")) 
        {
            NodeWindow.OpenWindow(nodesManager.gameObject);//打开窗口
            isEditor = true;//进入编辑状态
        }
        else if (isEditor && GUILayout.Button("结束编辑节点")) 
        {
            NodeWindow.CloseWindow();
            isEditor = false;
        }

        if (GUILayout.Button("删除最后一个节点"))
        {
        RemoveAtLastNode();
        }

        if (GUILayout.Button("删除所有节点"))
        {
        RemoveAllNodes();
        }
    }
    /// <summary>
    /// 终极数据类型绘制
    /// </summary>
    /// <param name="originalDataName"></param>
    /// <param name="processedHeadName"></param>
    void OtherDataDraw(string originalDataName, string processedHeadName)
    {
        //更新可序列化数据
        serializedObject.Update();
        //通过成员变量名找到组件上的成员变量
        SerializedProperty sp = serializedObject.FindProperty(originalDataName);
        //可序列化数据绘制（取到的数据，标题，是否将所有获得的序列化数据显示）
        EditorGUILayout.PropertyField(sp, new GUIContent(processedHeadName), true);
        //将修改的数据写入到可序列化的原始数据中
        serializedObject.ApplyModifiedProperties();

    }

  
    RaycastHit hit;

    //当选中关联的脚本挂载的物体，鼠标在Scene试图下发生变化时（鼠标移动点击时触发调用）
    private void OnSceneGUI()
    {
        if (!isEditor)//非编辑状态下直接返回 
          return;

        //鼠标按下左键时发射射线，非运行时使用Event类
        //Event.current.button:判断鼠标是哪个按键
        //Event.current.type:判断鼠标的事件方式
        if (Event.current.button == 0 && Event.current.type == EventType.MouseDown) 
        {
            //从鼠标位置发出射线
            //因为是从Scene视图下发射射线，跟场景中的摄像机并没有关系，所以不能使用相机发射射线的方法
            //从GUI中的一个点向世界定义一条射线，参数一般都是鼠标的坐标
     
            Ray ray=HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if(Physics.Raycast(ray,out hit, 100, 1 << 31)) 
            {
               InstancePathNode(hit.point + Vector3.up * 0.1f);
            }
        }
    }
    
    /// <summary>
    /// 创建新节点
    /// </summary>
    /// <param name="targetPos"></param>
    void InstancePathNode(Vector3 targetPos) 
    {
       
        GameObject prefab = Resources.Load<GameObject>("Prefabs/changli");
        GameObject newPathNode=Instantiate(prefab,targetPos,Quaternion.identity,nodesManager.transform);
        nodesManager.nodes.Add(newPathNode);//将生成的节点加入列表
    }

    //删除最后一个节点
    void RemoveAtLastNode()
    {
        if (nodesManager.nodes.Count > 0) 
        {
            Debug.Log("删除");
            //场景中删除倒数第一个节点物体
            DestroyImmediate(nodesManager.nodes[nodesManager.nodes.Count - 1]);
           //从列表中移除
             nodesManager.nodes.RemoveAt(nodesManager.nodes.Count - 1);
        }
    }
    //删除所有节点
    void RemoveAllNodes()
    {
        for (int i = 0; i < nodesManager.nodes.Count; i++) 
        {
            DestroyImmediate(nodesManager.nodes[i]);
        }
        nodesManager.nodes.Clear();
    }
}
