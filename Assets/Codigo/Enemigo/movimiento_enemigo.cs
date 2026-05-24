using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class MovimientoEnemigo : MonoBehaviour
{
    public GameObject a_quien_seguir;

    private NavMeshAgent control_movimiento;

    private ControlAtaquesEnemigo control_ataques;

    public bool recibiendo_golpe = false;

    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);
    public event cambio_estado_evento hay_gente_escuchando_el_estado;
    private EstadosMovimiento estado_actual = EstadosMovimiento.quieto;

    [Header("Hitboxes")]
    public GameObject hitbox_delantera_completa;
    public GameObject hitbox_trasera_completa;

    public GameObject hitbox_cabeza;

    void Start()
    {
        control_movimiento = GetComponent<NavMeshAgent>();

        control_ataques = GetComponent <ControlAtaquesEnemigo>();
    }

    void Update()
    {
        if(recibiendo_golpe)
        {
            Debug.Log("recibiendo golpe");
            cambiar_estado(EstadosMovimiento.quieto);
            return;
        }
        if(a_quien_seguir != null){
            Debug.Log($"[MovimientoEnenmigo][{gameObject.name}]");

            control_movimiento.destination = a_quien_seguir.transform.position;
        }
        else{
            control_movimiento.destination = gameObject.transform.position;
        } 

        // DETECTAR SI CAMINA O ESTA QUIETO
        if(control_movimiento.velocity.magnitude > 0.1f)
        {
            cambiar_estado(EstadosMovimiento.caminando);
        }
        else
        {
            cambiar_estado(EstadosMovimiento.quieto);
        }
    }

    //PARA REALIZAR LAS ANIMACIONES Y ATAQUES AUTOMATICOS

    void FixedUpdate(){
        /*if(Mathf.Abs(direccion_horizontal) < 0.1f)
        {
            cambiar_estado(EstadosMovimiento.quieto);
        }
        else
        {
            bool mirando_derecha = transform.forward.x > 0;

            bool retrocediendo = (mirando_derecha && direccion_horizontal < 0) || (!mirando_derecha && direccion_horizontal > 0);

            //if realizo un golpe cambiar estado a detenerse
            //if esta en detenerse cambiar a golpear de nuevo despues de un rato

            if(retrocediendo)
            {
                cambiar_estado(EstadosMovimiento.Retrocediendo);
            }
            else
            {
                cambiar_estado(EstadosMovimiento.caminando);
            }
        }

        Debug.Log($"El valor es: {direccion.magnitude}");
        if(direccion.magnitude > 0.1f){
            avanzar(direccion);
        }*/
    }
 
    void avanzar(Vector2 direccion_joystick)
    {
       //Vector3 movimiento = new Vector3(direccion_joystick.y, 0f, 0f);

        //rigid_body.MovePosition(transform.position + (movimiento * velocidad_movimiento * Time.fixedDeltaTime));
    }

    void cambiar_estado(EstadosMovimiento estado_nuevo)
    {
        estado_actual = estado_nuevo;

        if(hay_gente_escuchando_el_estado != null){
            hay_gente_escuchando_el_estado.Invoke(estado_nuevo);
        }
    }
}