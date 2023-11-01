using UnityEngine;
using UnityEngine.Audio;

public class audiomanager : MonoBehaviour
{
    [Header("------Audio Source------")]
    [SerializeField] AudioSource Musicsource;
    [SerializeField] AudioSource SFXsource;

    [Header("------Audio Clip------")]
    public AudioClip home;
    public AudioClip sfxc;

    private void Awake()
    {
        // Make sure the AudioManager persists across scenes
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Load audio settings from PlayerPrefs
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        // Load audio clip index and volume values from PlayerPrefs
        int audioClipIndex = PlayerPrefs.GetInt("AudioClipIndex", 0);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Set the audio clip and volume values
        Musicsource.clip = GetAudioClip(audioClipIndex);
        Musicsource.volume = musicVolume;
        Musicsource.Play();

        SFXsource.volume = sfxVolume;
    }

    public void playsfx(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }

    public void SetAudioClip(int index)
    {
        Musicsource.clip = GetAudioClip(index);
        PlayerPrefs.SetInt("AudioClipIndex", index);
    }

    public void SetMusicVolume(float volume)
    {
        Musicsource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        SFXsource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private AudioClip GetAudioClip(int index)
    {
        switch (index)
        {
            case 0:
                return home;
            case 1:
                return sfxc;
            default:
                return null;
        }
    }
}