using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class BasePanel : MonoBehaviour
{
    //��UI�����ø������洢����ʽ�滻ԭ�򣨸���װ���ࣩ
    public Dictionary<string, UIBehaviour> controlDic = new Dictionary<string, UIBehaviour>();

    //�ؼ�Ĭ�����֣�����õ��ؼ����ִ����������������ζ�Ų���ͨ��������ã�����ע��
    private static List<string> defaultNameList = new List<string>()
    {
        "Image",
        "Text(TMP)",
        "RawImage",
        "BackGround",
        "Checkmark",
        "Label",
        "Text(Legacy)",
        "Arrow",
        "Placeholder",
        "Fill",
        "Handle",
        "Viewport",
        "Scrollbar Horizontal",
        "Scrollbar Vertical",

    };
    protected virtual void Awake()
    {
        //Ϊ�˱���һ�������ϴ��ڶ���ؼ������Ȳ�����Ҫ�����
        FindChildrenControl<Button>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<InputField>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<Dropdown>();
        //*******************************************************
        FindChildrenControl<TextMeshPro>();
        FindChildrenControl<Text>();
        FindChildrenControl<Image>();
    }

    /// <summary>
    /// �����ʾ����
    /// </summary>
    public abstract void ShowPanel();
    /// <summary>
    /// �ر�������
    /// </summary>
    public abstract void HidePanel();

    /// <summary>
    /// ��ȡָ�������Լ�ָ�����͵�UI�ؼ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetControl<T>(string name) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(name))
        {
            T control = controlDic[name] as T;
            if (control == null)
                Debug.Log($"�����ڶ�Ӧ����Ϊ{name},����Ϊ{typeof(T)}�Ŀؼ�");
            return control;
        }
        else
        {
            Debug.Log($"�����ڶ�Ӧ����Ϊ{name}�Ŀؼ�");
            return null;
        }
    }

    protected virtual void ClickButton(string buttonName)
    {
    }
    protected void SliderValueChange(string sliderName, float value)
    {
    }
    protected void ToggleValueChange(string sliderName, bool value)
    {
    }

    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>(true);

        for (int i = 0; i < controls.Length; i++)
        {
            string controlName = controls[i].gameObject.name;
            //��һ�����͵�������������ֵ�
            if (!controlDic.ContainsKey(controls[i].gameObject.name))
            {
                if (!defaultNameList.Contains(controls[i].gameObject.name))
                {
                    controlDic.Add(controls[i].gameObject.name, controls[i]);
                    //�жϿؼ������� �����Ƿ�����¼�����
                    if (controls[i] is Button)
                    {
                        (controls[i] as Button).onClick.AddListener(() =>
                        {
                            ClickButton(controlName);
                        });
                    }
                    else if (controls[i] is Slider)
                    {
                        (controls[i] as Slider).onValueChanged.AddListener((value) =>
                        {
                            SliderValueChange(controlName, value);
                        });
                    }
                    else if (controls[i] is Toggle)
                    {
                        (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                        {
                            ToggleValueChange(controlName, value);
                        });

                    }
                }
            }
        }
    }
}
