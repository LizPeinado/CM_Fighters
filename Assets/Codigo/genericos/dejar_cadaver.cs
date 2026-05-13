using UnityEngine;

public class Dejar_cadaver : MonoBehaviour,MonitorMuerte
{

    public GameObject cadaver;


    public void procesar_muerte()
    {
        GameObject cadaver_abandonado = Instantiate(cadaver, transform.position, transform.rotation);

        Destroy (gameObject);
    }

}
