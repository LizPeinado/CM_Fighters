using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PantallaMorido : MonoBehaviour
{
    public GameObject pantalla_a_mostrar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(pantallas_espera());  
    }

    private IEnumerator pantallas_espera(){
        yield return new WaitForSeconds(3.5f);
        pantalla_a_mostrar.SetActive(true);
    }

}
