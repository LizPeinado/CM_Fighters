using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DejarCadaver : MonitorMuerte
{
    public GameObject cadaver;
    

    override public void procesar_muerte()
    {
        Debug.Log($"[DejarCadaver] Procesando muerte");

        GameObject cadaver_abandonado = Instantiate(cadaver, transform.position, transform.rotation);
        cadaver_abandonado.transform.SetParent(null);

        Destroy (gameObject);

        //SceneManager.LoadSceneAsync(2);
    }
}

/*[RequireComponent(typeof(AudioSource))]
public class SonidoMuerte : MonitorMuerte {
    public AudioClip sonido_de_tieso;

    public override void procesar_muerte() {
        GetComponent<AudioSource>().PlayOneShot(sonido_de_tieso);
    }
}*/