using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

public class MovimientoEnemigo : MonoBehaviour
{
    private Rigidbody rigid_body;
    public bool esta_saltando = false;
    public float fuerza_salto = 700f;

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
        rigid_body = GetComponent<Rigidbody>();

        control_movimiento = GetComponent<NavMeshAgent>();

        control_ataques = GetComponent <ControlAtaquesEnemigo>();
    }

    public void saltar()
    {
        if(esta_saltando) return;

        Ray rayo_hacia_el_suelo = new Ray(transform.position, Vector3.down);

        RaycastHit chocamos_con;

        if(Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con, 1.1f))
        {
            if(chocamos_con.collider.CompareTag("suelo"))
            {
                control_movimiento.enabled = false;
                rigid_body.AddForce(Vector3.up * fuerza_salto);
                esta_saltando = true;
                cambiar_estado(EstadosMovimiento.saltando);

                Debug.Log("ENEMIGO SALTO");
            }
        }
    }
    void OnCollisionEnter(Collision colision)
    {
        if(colision.gameObject.CompareTag("suelo"))
        {
            esta_saltando = false;
            control_movimiento.enabled = true;
            cambiar_estado(EstadosMovimiento.quieto);
        }
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

    void FixedUpdate(){
    
    }
 
    void cambiar_estado(EstadosMovimiento estado_nuevo)
    {
        estado_actual = estado_nuevo;

        if(hay_gente_escuchando_el_estado != null){
            hay_gente_escuchando_el_estado.Invoke(estado_nuevo);
        }
    }
}