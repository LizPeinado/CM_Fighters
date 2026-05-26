using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class sonido_tieso : MonoBehaviour
{
    public AudioClip sonido_de_tieso;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(sonido_de_tieso);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
