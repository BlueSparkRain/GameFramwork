using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public abstract class BasePanel : MonoBehaviour
{
    //将UI类型用父类来存储，里式替换原则（父类装子类）
    public Dictionary<string, UIBehaviour> controlDic = new Dictionary<string, UIBehaviour>();

    //控件默认名字，如果得到控件名字存在于这个容器，意味着不会通过代码调用，不需注册
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
        //为了避免一个对象上存在多个控件，优先查找重要的组件
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
    /// 面板显示调用
    /// </summary>
    public abstract void ShowPanel();
    /// <summary>
    /// 关闭面板调用
    /// </summary>
    public abstract void HidePanel();

    /// <summary>
    /// 获取指点名称以及指定类型的UI控件
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
                Debug.Log($"不存在对应名称为{name},类型为{typeof(T)}的控件");
            return control;
        }
        else
        {
            Debug.Log($"不存在对应名称为{name}的控件");
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
            //将一种类型的所有组件存入字典
            if (!controlDic.ContainsKey(controls[i].gameObject.name))
            {
                if (!defaultNameList.Contains(controls[i].gameObject.name))
                {
                    controlDic.Add(controls[i].gameObject.name, controls[i]);
                    //判断控件的类型 决定是否添加事件监听
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
