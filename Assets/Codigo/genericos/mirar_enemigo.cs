using UnityEngine;

public class MirarEnemigo : MonoBehaviour
{
    public Transform objetivo;

    void Update()
    {
        if (objetivo == null) return;

        // derecha
        if (objetivo.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        //izquierda
        else
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }
}