using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//����༭�������ռ䣬���������ڱ༭����������
using UnityEditor;
using System;
using NUnit.Framework.Internal;

//���༭�������ű�����Ҫ�༭������ű�������ҹ�����ϵ
//��ҽű��洢��EditorĿ¼�£����Բ��ᱻ�������յ���Ϸ��
[CustomEditor(typeof(Test))]
public class TestEditor : Editor
{
    //�����Ҫ�༭��ʾ�����
    private Test testComponent;
     
    //������������ڶ���ѡ�л���������ʱ����
    private void OnEnable()
    {
        testComponent = (Test)target;//�ڵ�ǰ����ҽű��л����Ҫ����չ��test�������
    }

    //������������ڶ���ȡ��ѡ�л�������㱻�Ƴ�ʱ����
    private void OnDisable()
    {
        testComponent = null;
    }
    //���ڻ��Ƽ��������������ں���
    public override void OnInspectorGUI()
    {
        //������ʾ
        EditorGUILayout.LabelField("������չ�ű�");

        //���������ͻ���////////////////////////////////////////////////////////////////////////////
        testComponent.ID=EditorGUILayout.IntField("��ɫID", testComponent.ID);
        testComponent.Name = EditorGUILayout.TextField("��ɫ����", testComponent.Name);
        testComponent.isDead = EditorGUILayout.Toggle("�Ƿ�����", testComponent.isDead);

        //�����������ͻ���/////////////////////////////////////////////////////////////////////////////
        testComponent.weapon = EditorGUILayout.ObjectField("����", testComponent.weapon, typeof(GameObject), true) as GameObject;
        testComponent.texture = EditorGUILayout.ObjectField("��ͼ", testComponent.texture, typeof(Texture), false) as Texture;
        //���⣬ԭʼ�����ֵ����Ա���������ͣ��Ƿ���Խ������ж����ϸ����������ע��������Դ��project���ǳ��������壬����false

        //ö���������ͻ���////////////////////////////////////////////////////////////////////////////
        testComponent.testEnum=(E_testEnum)EditorGUILayout.EnumPopup("���ְҵ",testComponent.testEnum);

        //�ռ��������ͻ��ƣ�������list�ȣ��Լ��Լ�д�����͵����ݣ�////////////////////////////////////////////////////////////////////////////
         OtherDataDraw("player", "���");

        //����������////////////////////////////////////////////////////////////////////////////
        testComponent.processSlider = EditorGUILayout.Slider(new GUIContent("������"), testComponent.processSlider, 0, 100);
        if(testComponent.processSlider >= 80) 
        {
            EditorGUILayout.HelpBox("��������������",MessageType.Error);  
        }
        if(testComponent.processSlider<=20)
        {
            EditorGUILayout.HelpBox("��������������", MessageType.Warning);
        }

        //��ť���ƣ�Ĭ��������ƣ�һ��ռһ����ť��
       if( GUILayout.Button("������ť"))
        {
            Debug.Log("����˰�ť");
        };

        //����������ƣ�һ�пɶ����ť��
        GUILayout.BeginHorizontal();  
        //�رպ������
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
        //���¿����л�����
        serializedObject.Update();
        //ͨ����Ա�������ҵ�����ϵĳ�Ա����
        SerializedProperty sp = serializedObject.FindProperty(originalDataName);
        //�����л����ݻ��ƣ�ȡ�������ݣ����⣬�Ƿ����л�õ����л�������ʾ��
        EditorGUILayout.PropertyField(sp, new GUIContent(processedHeadName), true);
        //���޸ĵ�����д�뵽�����л���ԭʼ������
        serializedObject.ApplyModifiedProperties();

    }
}
