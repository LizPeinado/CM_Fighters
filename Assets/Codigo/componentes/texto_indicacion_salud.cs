using UnityEngine;
using TMPro;

public class TextoIndicacionSalud : MonitorSalud
{
   private TMP_Text cajita_texto;

    void Start(){
        base.Inicializar();
        cajita_texto = GetComponentInChildren<TMP_Text>();
    }

    public override void actualizacion_salud(int cantidad_salud_nueva)
    {
        Debug.Log($"[TextoIndicacionSalud] {cantidad_salud_nueva}");
        cajita_texto.text = $"SALUD: {cantidad_salud_nueva} %";
    }
}
