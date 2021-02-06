using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    //将声音管理器写成单例模式
    public static AudioControl Instance;
    //音乐播放器
    public AudioSource MusicPlayer;
    //音效播放器
    public AudioSource SoundPlayer;
    void Start()
    {
        Instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //播放音乐
    public void PlayMusic(string name)
    {


        if (MusicPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Audio/Music/" + name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }
        else
        {
            if (MusicPlayer.clip.name != name)
            {
                MusicPlayer.Stop();
                AudioClip clip = Resources.Load<AudioClip>("Audio/Music/" + name);
                MusicPlayer.clip = clip;
                MusicPlayer.Play();
            }
        }
        Debug.Log("MusicPlayer.clip.name=" + MusicPlayer.clip.name);
    }

    public void StopMusic()
    {
        MusicPlayer.Stop();
    }

    //播放音效
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/Sound/" + name);
        SoundPlayer.clip = clip;
        SoundPlayer.PlayOneShot(clip);
    }

}
