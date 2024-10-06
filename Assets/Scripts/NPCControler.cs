using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPCControler : MonoBehaviour
{
    [Header("Movimiento Enemigo")]
    public Vector2 puntoA;
    public Vector2 puntoB;
    public float speed = 2f;
    private Vector2 direccion;
    private bool punto = true;
    private bool quieto = false;
    public float tiempoQuieto;
    public GameObject mensaje;
    void Start()
    {
        direccion = puntoB;
        mensaje.SetActive(false);
    }
    void Update()
    {
        if (!quieto)
        {
            transform.position = Vector2.MoveTowards(transform.position, direccion, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, direccion) < 0.1f)
            {
                if (punto)
                {
                    direccion = puntoA;
                    StartCoroutine(EsperarEnPunto());
                }
                else
                {
                    direccion = puntoB;
                    StartCoroutine(EsperarEnPunto());
                }
                punto = !punto;
            }
        }
    }
    private IEnumerator EsperarEnPunto()
    {
        quieto = true;
        yield return new WaitForSeconds(tiempoQuieto);
        quieto = false;
    }
    public void InteractNPC(InputAction.CallbackContext context)
    {
        mensaje.SetActive(true);
        quieto = true;
    }
}

