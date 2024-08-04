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

    //������������ڶ���ѡ�л����������ʱ����
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
        EditorGUILayout.LabelField("�������ӿ�");

        //���������ͻ���////////////////////////////////////////////////////////////////////////////
        testComponent.ID=EditorGUILayout.IntField("��ɫID", testComponent.ID);
        testComponent.Name = EditorGUILayout.TextField("��ɫ����", testComponent.Name);
        testComponent.isDead = EditorGUILayout.Toggle("�Ƿ�����", testComponent.isDead);

        //�����������ͻ���/////////////////////////////////////////////////////////////////////////////
        testComponent.weapon = EditorGUILayout.ObjectField("����", testComponent.weapon, typeof(GameObject), true) as GameObject;
        testComponent.texture = EditorGUILayout.ObjectField("��ͼ", testComponent.texture, typeof(Texture), true) as Texture;
        //���⣬ԭʼ�����ֵ����Ա���������ͣ��Ƿ���Խ������ϸ��������

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