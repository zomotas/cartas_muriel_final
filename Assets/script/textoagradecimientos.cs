using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textoagradecimientos : MonoBehaviour
{
    private TextMeshProUGUI textoAgradecimientos;
    public float duracionParpadeo = 2f; 
    public float duracionTransicion = 0.75f; 
    private Color colorOriginal;
    private bool parpadeando = true;
    private AudioManager audioManager;

    void Start()
    {
        textoAgradecimientos = GetComponent<TextMeshProUGUI>();
        if (textoAgradecimientos != null)
        {
            colorOriginal = textoAgradecimientos.color;
            StartCoroutine(ParpadearTexto());
        }
        else
        {
            Debug.LogError("No se encontró el componente TextMeshProUGUI en este GameObject.");
        }

        audioManager = AudioManager.Instance;
        audioManager.CambiarClipAgradecimientos();
    }

    void OnDestroy()
    {
        // Reanudar la música de fondo al salir de la escena de agradecimientos
        audioManager.ReanudarMusica();
    }

    IEnumerator ParpadearTexto()
    {
        while (parpadeando)
        {
            // Transición de opacidad hacia 0
            float tiempoDeshacer = 0f;
            while (tiempoDeshacer < duracionTransicion)
            {
                float alpha = Mathf.Lerp(1f, 0f, tiempoDeshacer / duracionTransicion);
                textoAgradecimientos.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, alpha);
                tiempoDeshacer += Time.deltaTime;
                yield return null;
            }

            // Transición de opacidad hacia 1
            tiempoDeshacer = 0f;
            while (tiempoDeshacer < duracionTransicion)
            {
                float alpha = Mathf.Lerp(0f, 1f, tiempoDeshacer / duracionTransicion);
                textoAgradecimientos.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, alpha);
                tiempoDeshacer += Time.deltaTime;
                yield return null;
            }
        }
    }

    public void DetenerParpadeo()
    {
        parpadeando = false;
        textoAgradecimientos.color = colorOriginal; 
    }

    public void DesactivarGameObject()
    {
        gameObject.SetActive(false);
    }

    public void ActivarGameObject()
    {
        gameObject.SetActive(true);
    }
}
