using UnityEngine;

/*public interface MonitorMuerte{
    void procesar_muerte();
}*/

public abstract class MonitorMuerte: MonoBehaviour {
    protected bool muerte_procesada = false;

    void Start(){
        var sistema_salud = GetComponent<Sistema_salud>();
        sistema_salud.quienes_quieren_saber_de_la_salud += actualizacion_salud;
    }

    /*protected void actualizacion_salud(int cantidad_salud_nueva){
        if(cantidad_salud_nueva <= 0){
            procesar_muerte();
        }
    }*/
    protected void actualizacion_salud(int cantidad_salud_nueva){
        if(muerte_procesada) return;

        if(cantidad_salud_nueva <= 0){
            muerte_procesada = true;
            procesar_muerte();
        }
    }

    abstract public void procesar_muerte();
}

