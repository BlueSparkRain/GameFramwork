using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : BaseSingletonManager<MusicManager>
{
    //�������ֲ������
    private AudioSource bkMusic = null;

    //�������ִ�С
    private float bkMusicValue = 0.1f;

    //������Ч��������Ķ���
    private GameObject soundObj = null;
    //�������ڲ��ŵ���Ч
    private List<AudioSource> soundList = new List<AudioSource>();
    //��Ч������С
    private float soundValue = 0.1f;
    //��Ч�Ƿ��ڲ���
    private bool soundIsPlay = true;


    private MusicManager()
    {
        MonoManager.Instance.AddFixedUpdateListener(Update);
    }


    private void Update()
    {
        if (!soundIsPlay)
            return;

        //��ͣ�ı������� �����û����Ч������� �������� ���Ƴ�������
        //Ϊ�˱���߱������Ƴ������� ���ǲ����������
        for (int i = soundList.Count - 1; i >= 0; --i)
        {
            if (!soundList[i].isPlaying)
            {
                GameObject.Destroy(soundList[i]);
                soundList.RemoveAt(i);
            }
        }
    }


    //���ű�������
    public void PlayBKMusic(string resName)
    {
        //��̬�������ű������ֵ���� ���� ����������Ƴ� 
        //��֤���������ڹ�����ʱҲ�ܲ���
        if (bkMusic == null)
        {
            GameObject obj = new GameObject();
            obj.name = "BKMusic";
            GameObject.DontDestroyOnLoad(obj);
            bkMusic = obj.AddComponent<AudioSource>();
        }

        //���ݴ���ı����������� �����ű�������
        ABManager.Instance.LoadResAsync<AudioClip>("music/bgm", resName, (clip) =>
        {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            bkMusic.volume = bkMusicValue;
            bkMusic.Play();
        },E_ABPlatformType.Window);
    }

    //ֹͣ��������
    public void StopBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Stop();
    }

    //��ͣ��������
    public void PauseBKMusic()
    {
        if (bkMusic == null)
            return;
        bkMusic.Pause();
    }

    //���ñ������ִ�С
    public void ChangeBKMusicValue(float v)
    {
        bkMusicValue = v;
        if (bkMusic == null)
            return;
        bkMusic.volume = bkMusicValue;
    }

    /// <summary>
    /// ������Ч
    /// </summary>
    /// <param name="name">��Ч����</param>
    /// <param name="isLoop">�Ƿ�ѭ��</param>
    /// <param name="isSync">�Ƿ�ͬ������</param>
    /// <param name="callBack">���ؽ�����Ļص�</param>
    public void PlaySound(string name, bool isLoop = false, bool isSync = false, UnityAction<AudioSource> callBack = null)
    {
        if (soundObj == null)
        {
            //��Ч�����Ķ��� һ���������Ч����Ҫֹͣ �������ǿ��Բ����������������Ƴ�
            soundObj = new GameObject("soundObj");
        }
        //������Ч��Դ ���в���
        ABManager.Instance.LoadResAsync<AudioClip>("music/sound", name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            //�洢���� ���ڼ�¼ ����֮���ж��Ƿ�ֹͣ
            soundList.Add(source);
            //���ݸ��ⲿʹ��
            callBack?.Invoke(source);
        }, E_ABPlatformType.Window);
    }

    /// <summary>
    /// ֹͣ������Ч
    /// </summary>
    /// <param name="source">��Ч�������</param>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            //ֹͣ����
            source.Stop();
            //���������Ƴ�
            soundList.Remove(source);
            //�������������Ƴ�
            GameObject.Destroy(source);
        }
    }

    /// <summary>
    /// �ı���Ч��С
    /// </summary>
    /// <param name="v"></param>
    public void ChangeSoundValue(float v)
    {
        soundValue = v;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = v;
        }
    }

    /// <summary>
    /// �������Ż�����ͣ������Ч
    /// </summary>
    /// <param name="isPlay">�Ƿ��Ǽ������� trueΪ���� falseΪ��ͣ</param>
    public void PlayOrPauseSound(bool isPlay)
    {
        if (isPlay)
        {
            soundIsPlay = true;
            for (int i = 0; i < soundList.Count; i++)
                soundList[i].Play();
        }
        else
        {
            soundIsPlay = false;
            for (int i = 0; i < soundList.Count; i++)
                soundList[i].Pause();
        }
    }
}
