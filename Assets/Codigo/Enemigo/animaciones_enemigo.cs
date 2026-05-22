using UnityEngine;

public class AnimacionesEnemigo : MonoBehaviour
{
    private Animator control_animacion;

    void Start()
    {
        control_animacion = GetComponent<Animator>();

        var control_de_movimiento = GetComponent<CerebroEnemigo>();
        control_de_movimiento.compoentes_escuchando_el_estado += al_cambiar_de_estado_del_enemigo;
    }

    void al_cambiar_de_estado_del_enemigo(EnemigoEstados nuevo_estado)
    {
        switch (nuevo_estado)
        {
            case EnemigoEstados.quieto:
                control_animacion.SetBool("esta_caminando", false);
            break;
        }
    }
}
