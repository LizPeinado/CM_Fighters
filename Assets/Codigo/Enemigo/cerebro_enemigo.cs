using UnityEngine;

public enum EnemigoEstados {
    quieto,
    atacar
}

[RequireComponent(typeof(MovimientoEnemigo))]

public class CerebroEnemigo : MonoBehaviour
{
    //ZombieEstados
    private EnemigoEstados estado_actual {get => _estado_actual; set{
        _estado_actual = value;
        compoentes_escuchando_el_estado?.Invoke(_estado_actual);
    }} 

    [SerializeField]
    private EnemigoEstados _estado_actual = EnemigoEstados.quieto;
    //public GameObject[] puntos_patrullaje;
    public GameObject comida_rapida;

    private GameObject objetivo_movimiento;

    private MovimientoEnemigo control_movimiento;

    public delegate void cambio_estado(EnemigoEstados estado_nuevo);

    public event cambio_estado compoentes_escuchando_el_estado;

    private float reloj = 0;
    
    void Start()
    {
        control_movimiento = GetComponent<MovimientoEnemigo>();

        control_movimiento.a_quien_seguir = null;

        //puntos_patrullaje = GameObject.FindGameObjectsWithTag("punto_patrullaje");

    }
    void Update()
    {
        switch (estado_actual)
        {
            case EnemigoEstados.quieto:
                estado_quieto();
                break;
            case EnemigoEstados.atacar:
                estado_atacar();
                break;
        }
    }
    void estado_atacar(){
        if(reloj <= 0.03F){
            Debug.Log($"[CerebroEnemigo] [{gameObject.name}] Estamos ATACANDO");
            var salud_enemiga = objetivo_movimiento?.GetComponent<Sistema_salud>();
            salud_enemiga?.restar_salud(150);

            //objetivo_movimiento = null;
            control_movimiento.a_quien_seguir = null;
        }
        else if (reloj > 2) {
            estado_actual = EnemigoEstados.quieto;
            reloj = 0;
        }
        reloj = reloj + Time.deltaTime;
    }

    void estado_quieto(){
        objetivo_movimiento=null;
        reloj = reloj + Time.deltaTime;
        Debug.Log($"La hora actua es {reloj}");
        if(reloj > 1F){
            reloj = 0;

            var resultado = Random.Range(0, 2);
            if(resultado > 0.5){
                estado_actual = EnemigoEstados.atacar;
            }
        }
    }
}
