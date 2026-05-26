using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioCamara : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip sonido_camara;
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sonido_camara;
        audioSource.loop = true;
        audioSource.Play();
    }
}