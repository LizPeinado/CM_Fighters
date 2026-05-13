using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MonitorMuerte))]

public class Sistema_salud : MonoBehaviour
{
    public int salud = 100;

    public UnityEvent evento;

    private int salud_restante{
        get => _salud_restante; 
        set{
            _salud_restante = value;

            if(_salud_restante < 0){
                quienes_quieren_saber_de_la_salud?.Invoke(0);
            }else{
                int porcentaje_salud = ( _salud_restante * 100) / salud;
                quienes_quieren_saber_de_la_salud?.Invoke(porcentaje_salud);
            }
        }        
    }

    
    private int _salud_restante = 0;
    private MonitorMuerte al_morir;

    public delegate void cambio_salud(int cantidad_actual_salud);

    public event cambio_salud quienes_quieren_saber_de_la_salud;

    void Start()
    {
        salud_restante = salud;
        //al_morir  = GetComponent<MonitorMuerte>();
        Debug.Log($"[Sistema_salud] Salud: {salud_restante}");
    }

    public void restar_salud(int cantidad){
        salud_restante = salud_restante - cantidad;
         Debug.Log($"[Sistema_salud] Salud: {salud_restante}");

        if (salud_restante < 0) {
            //Aqui deberia hacer algo para matar el obj
            //al_morir.procesar_muerte();
            Debug.Log("Llegamos al final de la vida del jugador");
        }
    }

}
