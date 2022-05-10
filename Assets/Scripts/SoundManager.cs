using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    public enum SoundTypes {Jump , Crash};
    public enum BgMusicTypes {MainBgMusic};
    
    public AudioSource jumpSound;
    public AudioSource crashSound;
    
    public AudioSource bgMusic;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //bgMusic.Play();
        //bgMusic.volume = 0.2f;
    }
    public void PlayBgMusic(BgMusicTypes currentMusic)
    {
        switch (currentMusic)
        {
            case BgMusicTypes.MainBgMusic:
                bgMusic.Play();
                bgMusic.volume = 0.1f;
                break;
        }
    }

    public void PlaySound(SoundTypes currentSound)
    {
        switch (currentSound)
        {
            case SoundTypes.Jump:
                jumpSound.Play();
                break;
            case SoundTypes.Crash:
                crashSound.Play();
                break;
        }
    }

    public void VoiceOff()
    {
        bgMusic.volume = 0.0f;
        jumpSound.volume = 0.0f;
        crashSound.volume = 0.0f;
    }
}
