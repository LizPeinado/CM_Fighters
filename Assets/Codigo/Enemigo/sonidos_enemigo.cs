using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SonidosEnemigo : MonoBehaviour
{
    public AudioClip estando_quieto;
    public AudioClip estando_atacando;
    private AudioSource fuente_sonido;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fuente_sonido = GetComponent<AudioSource>();

        var control_de_movimiento = GetComponent<CerebroEnemigo>();
        control_de_movimiento.compoentes_escuchando_el_estado += al_cambiar_de_estado_del_enemigo;
    }

    void al_cambiar_de_estado_del_enemigo(EnemigoEstados nuevo_estado){
        switch (nuevo_estado)
        {
            case EnemigoEstados.quieto:
                reproducir(estando_quieto);
            break;
            case EnemigoEstados.atacar:
                reproducir(estando_atacando);
                Invoke("detenerSonido", 2f); // se detiene despues de 1 segundo
            break;
        }
    }
    void reproducir(AudioClip Sonido){
        fuente_sonido.PlayOneShot(Sonido);
    }
    void detenerSonido(){
        fuente_sonido.Stop();
    }
}
