using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumeS : MonoBehaviour
{
    [SerializeField] private AudioMixer mainmixer;
    [SerializeField] private Slider masterslider;
    [SerializeField] private Slider musicslider;
    [SerializeField] private Slider sfxslider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetMusicVolume();
            SetSFXVolume();
        }

    }

    public void SetMasterVolume()
    {
        float volume = masterslider.value;
        mainmixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master", volume);
    }
    public void SetMusicVolume()
    {
        float volume = musicslider.value;
        mainmixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxslider.value;
        mainmixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    public void LoadVolume()
    {
        masterslider.value = PlayerPrefs.GetFloat("Master");
        musicslider.value = PlayerPrefs.GetFloat("Music");
        sfxslider.value = PlayerPrefs.GetFloat("SFX");
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
