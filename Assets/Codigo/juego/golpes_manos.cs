using System.Collections.Generic;
using UnityEngine;

public class GolpesManos : MonoBehaviour
{
    public bool ataque_activo = false;

    public int daño = 100;
    private List<GameObject> a_quien_le_pego;

    void Start()
    {
        a_quien_le_pego = new List<GameObject>();
    }

    void hacer_daño(GameObject a_quien){

        if (!ataque_activo) return;

        if (a_quien_le_pego.Contains(a_quien)) return;

        Sistema_salud salud_de_a_quien_golpeo = a_quien.GetComponent<Sistema_salud>();

        if(salud_de_a_quien_golpeo == null){

            //salud_de_a_quien_golpeo = a_quien.GetComponent<HitBox>().salud_del_padre;

            HitBox hitbox = a_quien.GetComponent<HitBox>();

            if(hitbox != null)
            {
                salud_de_a_quien_golpeo = hitbox.salud_del_padre;
            }
        }

        if(salud_de_a_quien_golpeo != null){
            if(a_quien.gameObject.CompareTag("jugador")){
                return;
            }
            salud_de_a_quien_golpeo.restar_salud(daño);

            HitBox hitbox_golpeada = a_quien.GetComponent<HitBox>();
            if(hitbox_golpeada != null)
            {
                hitbox_golpeada.empujar();
            }

            Invoke(nameof(limpiar_lista_golpes), 0.2f);

            a_quien_le_pego.Add(a_quien);
        }
    }

    /*void OnTriggerEnter(Collider golpear){
        hacer_daño(golpear.gameObject);
    }*/

    void OnTriggerStay(Collider golpear)
    {
        hacer_daño(golpear.gameObject);
    }

    void OnTriggerExit(Collider golpear){
        //hacer_daño(golpear.gameObject);
        a_quien_le_pego.Remove(golpear.gameObject);
    }

    void limpiar_lista_golpes()
    {
        a_quien_le_pego.Clear();
    }
}
