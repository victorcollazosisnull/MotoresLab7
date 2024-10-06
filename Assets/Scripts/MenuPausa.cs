using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public static MenuPausa Instance { get; private set; }
    [Header("Interfaz Menú")]
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    public AudioSource walkAudioSource;
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
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        walkAudioSource.Stop();
        menuPausa.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
}
