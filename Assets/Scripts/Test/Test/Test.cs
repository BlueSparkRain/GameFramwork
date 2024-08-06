using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int ID;
    public string Name;
    public bool isDead;

    public GameObject weapon;
    public Texture texture;
    public E_testEnum testEnum;
    public Player player;
    public float processSlider;

    void Start()
    {
       
        //  Instantiate( ABManager.Instance.LoadABRes<GameObject>("ui", "Sphere", E_ABPlatformType.Window));
        //ResourcesManager.Instance.LoadAsync<GameObject>("Cap", (a) => { Instantiate(a); });
        //ResourcesManager.Instance.LoadAsync("Cap", typeof(GameObject), (a) =>
        //{
        //    Instantiate(a);
        //});

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            UIManager.Instance.ShowPanel<TestPanel>(E_ABPlatformType.Window, E_UILayer.Top, (panel) =>
            {
                panel.TestFun();
            });
        }
        if (Input.GetKeyDown(KeyCode.H)) 
        {
            UIManager.Instance.HidePanel<TestPanel>();
        }
    }


}
public enum E_testEnum 
{
A,B,C
}
