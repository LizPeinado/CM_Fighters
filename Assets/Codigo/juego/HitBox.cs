using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Sistema_salud salud_del_padre;

    public bool es_hitbox_delantera = true;

    private Rigidbody rigidbody_del_padre;

    public float fuerza_empuje = .5f;

    void Start()
    {
        salud_del_padre = GetComponentInParent<Sistema_salud>();
        rigidbody_del_padre = GetComponentInParent<Rigidbody>();
    }

    public void empujar()
    {
        Debug.Log($"[{gameObject.name}] RECIBIO GOLPE");
        if(rigidbody_del_padre == null) return;
        Vector3 direccion_empuje;

        /*if(es_hitbox_delantera)
        {
            //direccion_empuje = Vector3.left;
            direccion_empuje = -transform.parent.forward;
            Debug.Log($"Direccion empuje_D: {direccion_empuje}");
        }
        else
        {
            //direccion_empuje = Vector3.right;
            direccion_empuje = transform.parent.forward;
            Debug.Log($"Direccion empuje_D: {direccion_empuje}");
        }*/
        bool mirando_derecha = transform.root.forward.x > 0;

        if(es_hitbox_delantera)
        {
            direccion_empuje =
            mirando_derecha ? Vector3.left : Vector3.right;
        }
        else
        {
            direccion_empuje =
            mirando_derecha ? Vector3.right : Vector3.left;
        }

        MovimientoEnemigo enemigo = GetComponentInParent<MovimientoEnemigo>();
        if(enemigo != null)
        {
            enemigo.recibiendo_golpe = true;
        }

        CancelInvoke(nameof(dejar_de_recibir_golpe));
        Invoke(nameof(dejar_de_recibir_golpe), 0.3f);

        Debug.Log($"POSICION ANTES: {rigidbody_del_padre.position}");
        /*rigidbody_del_padre.MovePosition(
            rigidbody_del_padre.position + (direccion_empuje * fuerza_empuje)
        );*/
        transform.root.position += direccion_empuje * fuerza_empuje;
    }

    void dejar_de_recibir_golpe()
    {
        MovimientoEnemigo enemigo =
        GetComponentInParent<MovimientoEnemigo>();

        if(enemigo != null)
        {
            enemigo.recibiendo_golpe = false;
        }
    }
}
