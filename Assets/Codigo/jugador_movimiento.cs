using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EstadosMovimiento{
    quieto,

    caminando,
    
    saltando,

    Retrocediendo,

    agachado
}

public class JugadorMovimiento : MonoBehaviour
{

    public float velocidad_movimiento = 5.0f;
    //public float velocidad_rotacion = 0.1f;
    //private float _velocidad_por_fotograma = 0.1f;

    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);
    public event cambio_estado_evento hay_gente_escuchando_el_estado;
    private EstadosMovimiento estado_actual = EstadosMovimiento.quieto;

    private Rigidbody rigid_body;
    private PlayerInput entradas_del_jugador;
    private InputAction movimiento;

    private InputAction saltar;

    private InputAction agacharse;
    public bool esta_agachado = false;
    public bool esta_saltando = false;

    [Header("Hitboxes")]
    public GameObject hitbox_delantera_completa;
    public GameObject hitbox_trasera_completa;

    public GameObject hitbox_delantera_agachado;
    public GameObject hitbox_trasera_agachado;

    public GameObject hitbox_cabeza;

    //private Animator animator;

    //private InputAction pegar_poquito;

    void Start(){
        entradas_del_jugador = GetComponent<PlayerInput>();
        rigid_body = GetComponent<Rigidbody>(); //referencia al primer rigidbody que encuentre
        
        movimiento = entradas_del_jugador.actions.FindAction("movimiento");

        //_velocidad_por_fotograma = velocidad_movimiento / 60;

        saltar = entradas_del_jugador.actions.FindAction("saltar");
        saltar.performed += salta_jugador_salta;

        agacharse = entradas_del_jugador.actions.FindAction("Agacharse");
        agacharse.performed += empezar_agacharse;
        agacharse.canceled += dejar_de_agacharse;

        actualizar_hitboxes();

        //animator = GetComponent<Animator>();
        /*pegar_poquito = entradas_del_jugador.actions.FindAction("GolpeLeve");
        pegar_poquito.performed += estoy_golpeando;*/
    }

    void salta_jugador_salta(InputAction.CallbackContext _){
        bool estamos_tocando_suelo = false;
        Ray rayo_hacia_el_suelo = new Ray(transform.position, transform.TransformDirection(Vector3.down));
        Debug.Log($"El rayo tiene de informacion {rayo_hacia_el_suelo}");

        RaycastHit chocamos_con;
        
        if(Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con, 1.1F)){

            Debug.Log($"Estoy chocando con {chocamos_con.collider.name}");

            if(chocamos_con.collider.CompareTag("suelo")){
                estamos_tocando_suelo = true;
            }

            if (estamos_tocando_suelo) { 
                rigid_body.AddForce(Vector3.up * 700f);

                esta_saltando = true;
                cambiar_estado(EstadosMovimiento.saltando);
            }
        }

        /*if(estamos_tocando_suelo){
            rigid_body.AddForce(Vector3.up * 550f);
        } */
    }

    //ESTA TOCANDO EL SUELO?
    void OnCollisionEnter(Collision colision)
    {
        if(colision.gameObject.CompareTag("suelo"))
        {
            esta_saltando = false;
        }
    }

    void empezar_agacharse(InputAction.CallbackContext _)
    {
        if(esta_saltando)
        {
            return;
        }
        esta_agachado = true;
        cambiar_estado(EstadosMovimiento.agachado);

        Debug.Log("Jugador agachado");

        actualizar_hitboxes();
    }

    void dejar_de_agacharse(InputAction.CallbackContext _)
    {
        esta_agachado = false;
        cambiar_estado(EstadosMovimiento.quieto);

        Debug.Log("Jugador dejo de agacharse");

        actualizar_hitboxes();
    }

    void actualizar_hitboxes()
    {
        if(esta_agachado)
        {
            hitbox_delantera_completa.SetActive(false);
            hitbox_trasera_completa.SetActive(false);

            hitbox_delantera_agachado.SetActive(true);
            hitbox_trasera_agachado.SetActive(true);
        }
        else
        {
            hitbox_delantera_completa.SetActive(true);
            hitbox_trasera_completa.SetActive(true);

            hitbox_delantera_agachado.SetActive(false);
            hitbox_trasera_agachado.SetActive(false);
        }
        hitbox_cabeza.SetActive(true);
    }

    void FixedUpdate(){
        if(esta_agachado)
        {
            return;
        }

        Vector2 direccion = movimiento.ReadValue<Vector2>();

        /*if(direccion.y > 0.1f)
        {
            cambiar_estado(EstadosMovimiento.caminando);
        }
        else if(direccion.y < -0.1f)
        {
            cambiar_estado(EstadosMovimiento.Retrocediendo);
        }
        else
        {
            cambiar_estado(EstadosMovimiento.quieto);
        }*/

        float direccion_horizontal = direccion.y;

        if(Mathf.Abs(direccion_horizontal) < 0.1f)
        {
            cambiar_estado(EstadosMovimiento.quieto);
        }
        else
        {
            bool mirando_derecha = transform.forward.x > 0;

            bool retrocediendo = 
            (mirando_derecha && direccion_horizontal < 0) ||
            (!mirando_derecha && direccion_horizontal > 0);

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
        }

        //avanzar(direccion);

        //rotar(direccion);
    }
 
    /*void avanzar(Vector2 direccion_joystick)
    {
        Vector3 hacia_adelante = transform.forward * direccion_joystick.y;
 
        rigid_body.MovePosition(transform.position + ((hacia_adelante * direccion_joystick.magnitude) * _velocidad_por_fotograma));
    }*/
    void avanzar(Vector2 direccion_joystick)
    {
        Vector3 movimiento = new Vector3(direccion_joystick.y, 0f, 0f);

        rigid_body.MovePosition(transform.position + (movimiento * velocidad_movimiento * Time.fixedDeltaTime));
    }

    void cambiar_estado(EstadosMovimiento estado_nuevo)
    {
        estado_actual = estado_nuevo;

        if(hay_gente_escuchando_el_estado != null){
            hay_gente_escuchando_el_estado.Invoke(estado_nuevo);
        }
    }

    /*void Update(){
        
    }*/

    // Update is called once per frame
    /*void Update(){

        Time.deltaTime;
        //encargado de actualizar cada que debe haber un movimiento, cosas que nos gustaria tener lo mas rapido posible

        //delta time: 
    }*/

    /*void rotar(Vector2 direccion_joystick)
    {
         float voltear = velocidad_rotacion * direccion_joystick.x;
 
        Quaternion rotacion =  Quaternion.Euler(0f, voltear, 0f);
 
        rigid_body.MoveRotation(transform.rotation * rotacion);
    }*/

    /*void animaciones(InputAction.CallbackContext _){
        Vector2 se_mueve = _.ReadValue<Vector2>();

        bool retroceder = se_mueve.y < 0;
        animator.SetBool("Retrocede", retroceder);
    }*/

}
