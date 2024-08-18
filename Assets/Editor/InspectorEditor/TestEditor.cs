using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引入编辑器命名空间，监视器属于编辑器开发范畴
using UnityEditor;
using System;
using NUnit.Framework.Internal;

//将编辑器开发脚本与需要编辑的组件脚本建立外挂关联关系
//外挂脚本存储于Editor目录下，所以不会被打入最终的游戏包
[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    //获得需要编辑显示的组件
    private Test testComponent;
     
    //当关联组件所在对象被选中或组件被添加时调用
    private void OnEnable()
    {
        testComponent = (Test)target;//在当前的外挂脚本中获得需要被拓展的test组件对象
    }

    //当关联组件所在对象被取消选中或组件爱你被移除时调用
    private void OnDisable()
    {
        testComponent = null;
    }
    //用于绘制检视面板的生命周期函数
    public override void OnInspectorGUI()
    {
        //标题显示
        EditorGUILayout.LabelField("测试拓展脚本");

        //简单数据类型绘制////////////////////////////////////////////////////////////////////////////
        testComponent.ID=EditorGUILayout.IntField("角色ID", testComponent.ID);
        testComponent.Name = EditorGUILayout.TextField("角色姓名", testComponent.Name);
        testComponent.isDead = EditorGUILayout.Toggle("是否死亡", testComponent.isDead);

        //对象数据类型绘制/////////////////////////////////////////////////////////////////////////////
        testComponent.weapon = EditorGUILayout.ObjectField("武器", testComponent.weapon, typeof(GameObject), true) as GameObject;
        testComponent.texture = EditorGUILayout.ObjectField("贴图", testComponent.texture, typeof(Texture), false) as Texture;
        //标题，原始组件的值，成员变量的类型，是否可以将场景中对象拖给这个变量，注意纹理来源于project而非场景中物体，故用false

        //枚举数据类型绘制////////////////////////////////////////////////////////////////////////////
        testComponent.testEnum=(E_testEnum)EditorGUILayout.EnumPopup("玩家职业",testComponent.testEnum);

        //终极数据类型绘制（适用于list等，以及自己写的类型的数据）////////////////////////////////////////////////////////////////////////////
         OtherDataDraw("player", "玩家");

        //滑动条绘制////////////////////////////////////////////////////////////////////////////
        testComponent.processSlider = EditorGUILayout.Slider(new GUIContent("滑动条"), testComponent.processSlider, 0, 100);
        if(testComponent.processSlider >= 80) 
        {
            EditorGUILayout.HelpBox("滑动条即将到顶",MessageType.Error);  
        }
        if(testComponent.processSlider<=20)
        {
            EditorGUILayout.HelpBox("滑动条即将到底", MessageType.Warning);
        }

        //按钮绘制（默认纵向绘制，一行占一个按钮）
       if( GUILayout.Button("来个按钮"))
        {
            Debug.Log("点击了按钮");
        };

        //开启横向绘制（一行可多个按钮）
        GUILayout.BeginHorizontal();  
        //关闭横向绘制
        GUILayout.EndHorizontal();

    }

    [MenuItem("UIPanel/Show")]
    static void ShowPanel()
    {
        UIManager.Instance.ShowPanel<TestPanel>(E_ABPlatformType.Window, E_UILayer.Top, (panel) =>
        {
            panel.TestFun();
        });
    }

    [MenuItem("UIPanel/Hide")]
    static void HidePanel()
    {
        UIManager.Instance.HidePanel<TestPanel>();
    }

    void OtherDataDraw(string originalDataName,string processedHeadName) 
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
}
