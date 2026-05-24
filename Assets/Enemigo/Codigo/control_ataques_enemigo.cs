using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EstadosAtaqueEnemigo
{
    esperando,
    atacando,
    muerto
}

public class ControlAtaquesEnemigo : MonoBehaviour
{
    public float tiempo_espera = 1f;
    private float tiempo_transcurrido = 0f;

    private Sistema_salud salud;
    private float tiempo_siguiente_ataque = 1f;

    public EstadosAtaqueEnemigo estado = EstadosAtaqueEnemigo.esperando;

    public bool golpe_debil = false;
    public bool golpe_fuerte = false;
    public bool patada_debil = false;
    public bool patada_fuerte  = false;

    public GolpesManos puño_izquierdo;
    public GolpesManos puño_derecho;

    public GolpesManos pie_izquierdo;
    public GolpesManos pie_derecho;

    private Animator animator;
    private bool siguiente_golpe_izquierdo = true;

    void Start()
    { 
        salud = GetComponent<Sistema_salud>();
        animator = GetComponent<Animator>();//referencia al primer Animator que encuentre

        /*
        Debug.Log(golpe_debil);
        Debug.Log(golpe_fuerte);
        Debug.Log(patada_debil);
        Debug.Log(patada_fuerte);
        */
    }

    /*void FixedUpdate(){
        switch(estado){
            case EstadosAtaqueEnemigo.ataque:
                tiempo_transcurrido += Time.deltaTime;
                if(tiempo_transcurrido > tiempo_espera){
                    tiempo_transcurrido = 0f;
                    estado = EstadosAtaqueEnemigo.espera;
                }
            break;
        }
    }*/

    void FixedUpdate()
    {
        switch(estado)
        {
            case EstadosAtaqueEnemigo.esperando:
                estado_esperando();
            break;
            case EstadosAtaqueEnemigo.atacando:
                estado_atacando();
            break;
        }
    }

    void estado_esperando()
    {
        tiempo_transcurrido += Time.deltaTime;
        if(tiempo_transcurrido >= tiempo_siguiente_ataque)
        {
            tiempo_transcurrido = 0f;
            elegir_ataque();
            estado = EstadosAtaqueEnemigo.atacando;
        }
    }

    void estado_atacando()
    {
        tiempo_transcurrido += Time.deltaTime;

        if(tiempo_transcurrido >= 1f)
        {
            tiempo_transcurrido = 0f;
            calcular_nuevo_tiempo();
            estado = EstadosAtaqueEnemigo.esperando;
        }
    }

    void elegir_ataque()
    {
        JugadorMovimiento jugador = FindFirstObjectByType<JugadorMovimiento>();

        bool jugador_agachado = false;
        if(jugador != null)
        {
            jugador_agachado = jugador.esta_agachado;
        }

        int ataque;
        if(jugador_agachado)
        {
            ataque = Random.Range(0,2);

            if(ataque == 0)
            {
                hacer_patada_debil();
            }
            else
            {
                hacer_patada_fuerte();
            }
        }
        else
        {
            ataque = Random.Range(0,4);
            switch(ataque)
            {
                case 0:
                    hacer_golpe_debil();
                break;

                case 1:
                    hacer_golpe_fuerte();
                break;

                case 2:
                    hacer_patada_debil();
                break;

                case 3:
                    hacer_patada_fuerte();
                break;
            }
        }
    }

    void calcular_nuevo_tiempo()
    {
        int vida_actual = salud.salud;
        if(vida_actual > 700)
        {
            tiempo_siguiente_ataque = 1f;
        }
        else if(vida_actual > 400)
        {
            tiempo_siguiente_ataque = .5f;
        }
        else
        {
            tiempo_siguiente_ataque = .2f;
        }
    }

    void cambiara_estado_ataque(){
        estado = EstadosAtaqueEnemigo.atacando;
    }

    /*
    void hacer_golpe_debil(InputAction.CallbackContext _)
    {
        StartCoroutine(activar_ataque(puño_izquierdo,puño_derecho,40,0.2f));
    }
    */
    public void hacer_golpe_debil()
    {
        cambiara_estado_ataque();

        if(siguiente_golpe_izquierdo)
        {
            animator.SetTrigger("Golpe2");
        }
        else
        {
            animator.SetTrigger("Golpe3");
        }

        siguiente_golpe_izquierdo = !siguiente_golpe_izquierdo;
        StartCoroutine(activar_ataque(puño_izquierdo, puño_derecho, 10, 2f));
    }

    public void hacer_golpe_fuerte()
    {
        cambiara_estado_ataque();

        animator.SetTrigger("GolpeFuerte");
        StartCoroutine(activar_ataque(puño_izquierdo, puño_derecho, 30, 1f));
    }

    public void hacer_patada_debil()
    {
        cambiara_estado_ataque();

        animator.SetTrigger("PatadaDebil");
        StartCoroutine(activar_ataque(pie_izquierdo, pie_derecho, 20, 1f));
    }

    public void hacer_patada_fuerte()
    {
        cambiara_estado_ataque();

        animator.SetTrigger("PatadaFuerte");
        StartCoroutine(activar_ataque(pie_izquierdo, pie_derecho, 30, 0.35f));
    }

    IEnumerator activar_ataque(GolpesManos golpe1, GolpesManos golpe2, int daño, float duracion)
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