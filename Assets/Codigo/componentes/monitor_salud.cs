using UnityEngine;
using TMPro;

public abstract class MonitorSalud : MonoBehaviour
{
    public GameObject monitorear_a = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Inicializar()
    {
        Sistema_salud sistema_salud;

        if(monitorear_a != null){
            sistema_salud = monitorear_a.GetComponent<Sistema_salud>();

        }
        else{
            sistema_salud = transform.parent.GetComponent<Sistema_salud>();
        }
        
        sistema_salud.quienes_quieren_saber_de_la_salud += actualizacion_salud;

    }

    // Update is called once per frame
    abstract public void actualizacion_salud(int cantidad_salud_nueva);
}
