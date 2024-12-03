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

    private void Start()
    {
        musicslider.value = PlayerPrefs.GetFloat("Music", 1f);
        SFXlider.value = PlayerPrefs.GetFloat("SFX", 1f);
        audioMixer.SetFloat("Music", Mathf.Log10(musicslider.value) * 20);
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXlider.value) * 20);
    }
    public void SetMusicVolume(float musicvolume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(musicvolume) * 20);
        PlayerPrefs.SetFloat("Music", musicvolume);

    }

    public void SetSFXVolume()
    { 
        float SFXvolume = SFXlider.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(SFXvolume) * 20);
    }
}
