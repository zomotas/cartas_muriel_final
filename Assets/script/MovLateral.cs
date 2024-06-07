using UnityEngine;

public class MovLateral : MonoBehaviour
{
    public float velocidad = 1f; 
    public float amplitud = 0.5f; 
    private Vector3 posicionInicial; 

    void Start()
    {
        
        posicionInicial = transform.position;
    }

    void Update()
    {
        
        float desplazamiento = Mathf.Sin(Time.time * velocidad) * amplitud;
        transform.position = posicionInicial + Vector3.right * desplazamiento;
    }
}
