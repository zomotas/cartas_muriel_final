using System.Collections;
using UnityEngine;

public class rotar : MonoBehaviour
{
    public float velocidadRotar = 10f; // Velocidad de rotación en grados por segundo
    public float intervaloParpadeo = 2f; // Intervalo de tiempo entre cada parpadeo largo
    public float duracionInvisible = 5f; // Duración que el objeto permanece invisible
    public int numeroParpadeos = 2; // Número de parpadeos rápidos
    public float duracionParpadeoRapido = 0.01f; // Duración del parpadeo rápido

    private Renderer objectoRenderer; // Renderer del objeto
    private bool parpadeando = true; // Para controlar el parpadeo

    void Start()
    {
        // Obtener el componente Renderer
        objectoRenderer = GetComponent<Renderer>();
        if (objectoRenderer != null)
        {
            StartCoroutine(Parpadeo());
        }
        else
        {
            Debug.LogError("No se encontró el componente Renderer en este GameObject.");
        }
    }

    void Update()
    {
        transform.Rotate(0, velocidadRotar * Time.deltaTime, 0);
    }

    IEnumerator Parpadeo()
    {
        while (parpadeando)
        {
            
            objectoRenderer.enabled = false;
            yield return new WaitForSeconds(duracionInvisible);

          
            objectoRenderer.enabled = true;
            yield return new WaitForSeconds(duracionParpadeoRapido/20);

            
            for (int i = 0; i < numeroParpadeos; i++)
            {
                objectoRenderer.enabled = false;
                yield return new WaitForSeconds(duracionParpadeoRapido);
                objectoRenderer.enabled = true;
                yield return new WaitForSeconds(duracionParpadeoRapido);
            }

            
            yield return new WaitForSeconds(intervaloParpadeo);
        }
    }
}
