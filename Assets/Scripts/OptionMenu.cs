using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicslider;
    [SerializeField] private Slider SFXlider;

    public void SetMusicVolume()
    {
        float musicvolume = musicslider.value;
        audioMixer.SetFloat("Music", Mathf.Log10(musicvolume) * 20);
    }

    public void SetSFXVolume()
    { 
        float SFXvolume = SFXlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXvolume) * 20);
    }
}
