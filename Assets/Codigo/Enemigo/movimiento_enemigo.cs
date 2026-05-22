using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class MovimientoEnemigo : MonoBehaviour
{
    public GameObject a_quien_seguir;

    private NavMeshAgent control_movimiento;

    public bool recibiendo_golpe = false;

    void Start()
    {
        control_movimiento = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(recibiendo_golpe)
        {
            Debug.Log("recibiendo golpe");
            return;
        }
        if(a_quien_seguir != null){
            Debug.Log($"[MovimientoEnenmigo][{gameObject.name}]");
            control_movimiento.destination = a_quien_seguir.transform.position;
        }
        else{
            control_movimiento.destination = gameObject.transform.position;
        } 
    }
}