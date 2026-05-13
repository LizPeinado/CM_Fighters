using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EstadosMovimiento{
    quieto,

    caminando,
    
    saltando,

    Retrocediendo

    //golpe_leve
}

public class JugadorMovimiento : MonoBehaviour
{

    public float velocidad_movimiento = 5.0f;
    public float velocidad_rotacion = 0.1f;
    private float _velocidad_por_fotograma = 0.1f;

    public delegate void cambio_estado_evento(EstadosMovimiento estado_nuevo);

    public event cambio_estado_evento hay_gente_escuchando_el_estado;

    private EstadosMovimiento estado_actual = EstadosMovimiento.quieto;

    private Rigidbody rigid_body;
    private PlayerInput entradas_del_jugador;
    private InputAction movimiento;

    private InputAction saltar;


    //private Animator animator;

    //private InputAction pegar_poquito;
    void Start(){
        entradas_del_jugador = GetComponent<PlayerInput>();
        rigid_body = GetComponent<Rigidbody>(); //referencia al primer rigidbody que encuentre
        
        movimiento = entradas_del_jugador.actions.FindAction("movimiento");

        _velocidad_por_fotograma = velocidad_movimiento / 60;

        saltar = entradas_del_jugador.actions.FindAction("saltar");
        saltar.performed += salta_jugador_salta;

        //animator = GetComponent<Animator>();

        /*pegar_poquito = entradas_del_jugador.actions.FindAction("GolpeLeve");
        pegar_poquito.performed += estoy_golpeando;*/
    }

    void salta_jugador_salta(InputAction.CallbackContext _){
        bool estamos_tocando_suelo = false;
        Ray rayo_hacia_el_suelo = new Ray(transform.position, new Vector3( 0, -0.9f,0));
        Debug.Log($"El rayo tiene de informacion {rayo_hacia_el_suelo}");

        RaycastHit chocamos_con;
        
        if(Physics.Raycast(rayo_hacia_el_suelo, out chocamos_con)){

            Debug.Log($"Estoy chocando con {chocamos_con.collider.name}");
            if(chocamos_con.collider.CompareTag("suelo")){
                estamos_tocando_suelo = true;
            }
        }

        if(estamos_tocando_suelo){
            rigid_body.AddForce(Vector3.up * 550f);
        } 
    }

    void FixedUpdate(){
        Vector2 direccion = movimiento.ReadValue<Vector2>();

        /*Debug.Log(direccion.y);
        animator.SetBool("Retrocede", direccion.y < 0);*/

        if(direccion.magnitude > 0.1f){
            cambiar_estado(EstadosMovimiento.caminando);

        }
        else{
            cambiar_estado(EstadosMovimiento.quieto);
        }

        Debug.Log($"El valor es: {direccion.magnitude}");
       
        if(direccion.magnitude > 0.1f){
            avanzar(direccion);
        }

        //avanzar(direccion);

        //rotar(direccion);
    }
 
    void avanzar(Vector2 direccion_joystick)
    {
        Vector3 hacia_adelante = transform.forward * direccion_joystick.y;
 
        rigid_body.MovePosition(transform.position + ((hacia_adelante * direccion_joystick.magnitude) * _velocidad_por_fotograma));
    }

    /*void animaciones(InputAction.CallbackContext _){
        Vector2 se_mueve = _.ReadValue<Vector2>();

        bool retroceder = se_mueve.y < 0;
        animator.SetBool("Retrocede", retroceder);
    }*/

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
}
