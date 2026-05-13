using UnityEngine;
using UnityEngine.UI;

public class BarraSalud : MonitorSalud
{
    private Slider barra;

    void Start(){
        base.Inicializar();

        barra = GetComponentInChildren<Slider>();

    }

    public override void actualizacion_salud(int cantidad_salud_nueva){
        barra.value = cantidad_salud_nueva;
        Debug.Log($"[BarraSalud] Salud: {cantidad_salud_nueva}");

    }
}