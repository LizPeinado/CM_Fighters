using System.Collections.Generic;
using UnityEngine;

public class GolpesManos : MonoBehaviour
{
    public int daño = 100;
    private List<GameObject> a_quien_le_pego;

    void Start()
    {
        a_quien_le_pego = new List<GameObject>();
    }

    void hacer_daño(GameObject a_quien){
        Sistema_salud salud_de_a_quien_golpeo = a_quien.GetComponent<Sistema_salud>();

        if(salud_de_a_quien_golpeo == null){
            salud_de_a_quien_golpeo = a_quien.GetComponent<HitBox>().salud_del_padre;
        }

        if(salud_de_a_quien_golpeo != null){
            if(a_quien.gameObject.CompareTag("jugador")){
                return;
            }
            salud_de_a_quien_golpeo.restar_salud(daño);
        }
    }

    void OnTriggerEnter(Collider golpear){
        hacer_daño(golpear.gameObject);
    }

    void OnTriggerExit(Collider golpear){
        hacer_daño(golpear.gameObject);
    }
}
