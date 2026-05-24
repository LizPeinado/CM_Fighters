using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlAtaques : MonoBehaviour
{
    private PlayerInput entradas;

    private InputAction golpe_debil;
    private InputAction golpe_fuerte;
    private InputAction patada_debil;
    private InputAction patada_fuerte;

    public GolpesManos puño_izquierdo;
    public GolpesManos puño_derecho;

    public GolpesManos pie_izquierdo;
    public GolpesManos pie_derecho;

    private Animator animator;
    private bool siguiente_golpe_izquierdo = true;

    void Start()
    {
        entradas = GetComponent<PlayerInput>();

        golpe_debil = entradas.actions.FindAction("GolpeDebil");
        golpe_fuerte = entradas.actions.FindAction("GolpeFuerte");

        patada_debil = entradas.actions.FindAction("PatadaDebil");
        patada_fuerte = entradas.actions.FindAction("PatadaFuerte");

        golpe_debil.performed += hacer_golpe_debil;
        golpe_fuerte.performed += hacer_golpe_fuerte;

        patada_debil.performed += hacer_patada_debil;
        patada_fuerte.performed += hacer_patada_fuerte;

        if(golpe_fuerte.IsPressed() && patada_fuerte.IsPressed())
        {
            Debug.Log("ATAQUE ESPECIAL");
        }

        
        animator = GetComponent<Animator>();//referencia al primer Animator que encuentre

        /*
        Debug.Log(golpe_debil);
        Debug.Log(golpe_fuerte);
        Debug.Log(patada_debil);
        Debug.Log(patada_fuerte);
        */
    }

    /*
    void hacer_golpe_debil(InputAction.CallbackContext _)
    {
        StartCoroutine(activar_ataque(puño_izquierdo,puño_derecho,40,0.2f));
    }
    */
    void hacer_golpe_debil(InputAction.CallbackContext _)
    {
        if(siguiente_golpe_izquierdo)
        {
            animator.SetTrigger("Golpe2");
        }
        else
        {
            animator.SetTrigger("Golpe3");
        }

        siguiente_golpe_izquierdo = !siguiente_golpe_izquierdo;
        StartCoroutine(activar_ataque(puño_izquierdo,puño_derecho,10,0.2f));
    }

    void hacer_golpe_fuerte(InputAction.CallbackContext _)
    {
        animator.SetTrigger("GolpeFuerte");
        StartCoroutine(activar_ataque(puño_izquierdo,puño_derecho,30,1f));
    }

    void hacer_patada_debil(InputAction.CallbackContext _)
    {
        animator.SetTrigger("PatadaDebil");
        StartCoroutine(activar_ataque(pie_izquierdo,pie_derecho,20,1f));
    }

    void hacer_patada_fuerte(InputAction.CallbackContext _)
    {
        animator.SetTrigger("PatadaFuerte");
        StartCoroutine(activar_ataque(pie_izquierdo,pie_derecho,60,0.35f));
    }

    IEnumerator activar_ataque(GolpesManos golpe1,GolpesManos golpe2,int daño,float duracion)
    {
        golpe1.daño = daño;
        golpe2.daño = daño;

        golpe1.ataque_activo = true;
        golpe2.ataque_activo = true;

        yield return new WaitForSeconds(duracion);

        golpe1.ataque_activo = false;
        golpe2.ataque_activo = false;
    }
}