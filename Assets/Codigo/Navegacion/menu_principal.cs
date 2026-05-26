using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_principal : MonoBehaviour
{
   private int nivel_actual = 1;
   private Scene escena_actual;
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

    public void SiguienteNivel(){
        escena_actual = SceneManager.GetActiveScene(); //Conseguir escena actual
        nivel_actual = escena_actual.buildIndex + 1; //Aumentar valor para pasar al siguiente
        if(nivel_actual > 3){ nivel_actual = 0; } // Max 3 escenas, si es más de 4 regresa al menu
        SceneManager.LoadSceneAsync(nivel_actual);
    }

}
