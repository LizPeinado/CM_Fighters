using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Viento : MonoBehaviour
{
    [SerializeField]
    
    float FuerzaViento = 0;

    private void OnTriggerStay(Collider other){
        var Objeto_a_Pegar = other.gameObject;

        if(Objeto_a_Pegar != null)
        {
            var rigid = Objeto_a_Pegar.GetComponent<Rigidbody>();
            var direccion = transform.up;
            rigid.AddForce(direccion * FuerzaViento);
        }
    } 
}
