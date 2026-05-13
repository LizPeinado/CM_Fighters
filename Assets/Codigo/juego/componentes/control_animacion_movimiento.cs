using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ControlAnimacionMovimiento : MonoBehaviour
{
    private Animator control_animacion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        control_animacion = GetComponent<Animator>();

        var control_de_movimiento = GetComponent<JugadorMovimiento>();
        control_de_movimiento.hay_gente_escuchando_el_estado += al_cambiar_de_estado_del_control_de_movimiento;

    }

    // Update is called once per frame

    void al_cambiar_de_estado_del_control_de_movimiento(EstadosMovimiento nuevo_estado)
    {
        switch (nuevo_estado)
        {
            case EstadosMovimiento.quieto:
                control_animacion.SetBool("Quieto", true);
            break;
            
            case EstadosMovimiento.caminando:
                control_animacion.SetBool("Quieto", false);
            break;

            case EstadosMovimiento.Retrocediendo:
                control_animacion.SetBool("Retrocede", true);
            break;

            case EstadosMovimiento. saltando:
            //case EstadosMovimiento.saltando:
            break;
            
        }
    }
}
