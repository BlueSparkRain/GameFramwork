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
        ResourcesManager.Instance.LoadAsync<GameObject>("Cap", (a) => { Instantiate(a); });
        ResourcesManager.Instance.LoadAsync("Cap",typeof(GameObject), (a) => { Instantiate(a);
        });

    }

  
}
public enum E_testEnum 
{
A,B,C
}
