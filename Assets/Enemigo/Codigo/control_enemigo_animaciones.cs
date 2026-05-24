using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ControlEnemigoAnimaciones : MonoBehaviour
{
    private Animator control_animacion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        control_animacion = GetComponent<Animator>();
        //AQUI LLAMA A JUGADOR MOVIMIENTO
        var control_de_movimiento = GetComponent<MovimientoEnemigo>();
        control_de_movimiento.hay_gente_escuchando_el_estado += al_cambiar_de_estado_del_control_de_movimiento;

    }

    // Update is called once per frame

    void al_cambiar_de_estado_del_control_de_movimiento(EstadosMovimiento nuevo_estado)
    {
        control_animacion.SetBool("Quieto", false);
        control_animacion.SetBool("Retrocede", false);

        switch (nuevo_estado)
        {
            case EstadosMovimiento.quieto:
                control_animacion.SetBool("Quieto", true);
            break;
            
            case EstadosMovimiento.caminando:
                control_animacion.SetBool("Quieto", false);
            break;

            case EstadosMovimiento.saltando:
            break;
        }
    }
}