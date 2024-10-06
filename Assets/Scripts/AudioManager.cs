using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public VolumenSettings volumenSettings;
    public MusicRed musicRed;
    public MusicBlue musicBlue;
    public MusicPurple musicPurple;
    public AudioMixer audioMixer;
    public AudioSource walkSource;
    public AudioSource enterSource;
    public AudioSource exitSource;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
    }
    void Start()
    {
        masterSlider.value = 1;
        musicSlider.value = 1;
        sfxSlider.value = 1;

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        Volumes();
    }

    public void SetMasterVolume(float volume)
    {
        volumenSettings.masterVolumen = volume;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        volumenSettings.musicVolumen = volume;
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        volumenSettings.sfxVolumen = volume;
        audioMixer.SetFloat("Sounds", Mathf.Log10(volume) * 20);
    }

    public void Volumes()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volumenSettings.masterVolumen) * 20);
        audioMixer.SetFloat("Music", Mathf.Log10(volumenSettings.musicVolumen) * 20);
        audioMixer.SetFloat("Sounds", Mathf.Log10(volumenSettings.sfxVolumen) * 20);
    }
}

