using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }
    [Header("Movimiento")]
    [SerializeField] private float speed;
    private Vector2 movementInput;
    private Rigidbody _rigidbody;
    [Header("Sonidos")]
    public AudioSource walkAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource enterAudioSource;
    public AudioSource exitAudioSource;
    public FadeInFadeOut fadeInFadeOut;
    [Header("Musicas de Cuartos")]
    public VolumenSettings volumenSettings;
    public MusicRed musicRed;
    public MusicBlue musicBlue;
    public MusicPurple musicPurple;
    public NPCControler npc;

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
        _rigidbody = GetComponent<Rigidbody>();
        fadeInFadeOut = GetComponent<FadeInFadeOut>(); 
    }
    public void ReadDirection(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
    public void InteractNPC(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            npc.InteractNPC(context);
        }
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementInput.x, movementInput.y, 0) * speed;
        _rigidbody.velocity = movement;
        if (movementInput != Vector2.zero)
        {
            if (!walkAudioSource.isPlaying)
            {
                walkAudioSource.Play();
            }
        }
        else
        {
            walkAudioSource.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rojo") || other.CompareTag("Azul") || other.CompareTag("Morado"))
        {
            enterAudioSource.Play();
            if (other.CompareTag("Rojo") && musicAudioSource.clip != musicRed.redMusic)
            {
                musicAudioSource.clip = musicRed.redMusic;
            }
            else if (other.CompareTag("Azul") && musicAudioSource.clip != musicBlue.blueMusic)
            {
                musicAudioSource.clip = musicBlue.blueMusic;
            }
            else if (other.CompareTag("Morado") && musicAudioSource.clip != musicPurple.purpleMusic)
            {
                musicAudioSource.clip = musicPurple.purpleMusic;
            }
            musicAudioSource.loop = true;
            musicAudioSource.Play(); 

            fadeInFadeOut.Fade(1f); 
            StartCoroutine(FadeAfter()); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rojo") || other.CompareTag("Azul") || other.CompareTag("Morado"))
        {
            exitAudioSource.Play();
            fadeInFadeOut.Fade(1f); 
            StartCoroutine(FadeAfter()); 
            exitAudioSource.Play();
            musicAudioSource.Stop();
        }
    }

    private IEnumerator FadeAfter()
    {
        yield return new WaitForSeconds(fadeInFadeOut.fadeDuration); 
        fadeInFadeOut.Fade(0f); 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cuarto"))
        {
            SceneManager.LoadScene("nivel2");
        }
    }
}
