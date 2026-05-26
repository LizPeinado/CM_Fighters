using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_principal : MonoBehaviour
{
   public void ComenzarJuego(){
        SceneManager.LoadSceneAsync(1);
    }

    public void EmpezarNivel1(){
        SceneManager.LoadSceneAsync(1);
    }

    public void EmpezarNivel2(){
        SceneManager.LoadSceneAsync(2);
    }

    public void EmpezarNivel3(){
        SceneManager.LoadSceneAsync(3);
    }
        
    public void MenuPrincipal(){
        SceneManager.LoadSceneAsync(0);
    }
}
