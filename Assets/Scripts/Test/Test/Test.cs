using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    float v;
    float oldv;

    void Start()
    {
       
        //  Instantiate( ABManager.Instance.LoadABRes<GameObject>("ui", "Sphere", E_ABPlatformType.Window));
        //ResourcesManager.Instance.LoadAsync<GameObject>("Cap", (a) => { Instantiate(a); });
        //ResourcesManager.Instance.LoadAsync("Cap", typeof(GameObject), (a) =>
        //{
        //    Instantiate(a);
        //});

    }
    private void OnGUI()
    {
        if (GUILayout.Button("²¥·Åbgm1"))
        {
            MusicManager.Instance.PlayBKMusic("testbgm1");
        }
        if (GUILayout.Button("²¥·Åbgm2"))
        {
            MusicManager.Instance.PlayBKMusic("testbgm1");
        }
        if (GUILayout.Button("²¥·Åsound1"))
        {
            MusicManager.Instance.PlaySound("testsound1");
        }

        if (GUILayout.Button("Í£Ö¹bgm²¥·Å"))
        {
            MusicManager.Instance.StopBKMusic();
        }
        if (GUILayout.Button("ÔÝÍ£bgm²¥·Å"))
        {
            MusicManager.Instance.PauseBKMusic();

        }
         v= GUILayout.HorizontalSlider(v, 0, 1);
        if (oldv !=v)
        {
            oldv = v;
            MusicManager.Instance.ChangeBKMusicValue(v);

        }
        if (GUILayout.Button("ÔÝÍ£sound²¥·Å"))
        {
            MusicManager.Instance.PlayOrPauseSound(false);
        }
        if (GUILayout.Button("¼ÌÐøsound²¥·Å"))
        {
            MusicManager.Instance.PlayOrPauseSound(true);
        }

    }
    
    private void Update()
    {
       
    }


}


public enum E_testEnum 
{
A,B,C
}
