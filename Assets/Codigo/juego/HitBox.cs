using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Sistema_salud salud_del_padre;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        salud_del_padre = GetComponentInParent<Sistema_salud>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
