using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class texto : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public float parpadeoDuracion = 2f; 
    public float transicionDuracion = 0.75f; 
    private Color originalColor;
    private bool estaParpadeando = true;

    void Start()
    {
        // Obtener el componente TextMeshProUGUI
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color;
            StartCoroutine(textoParpadeo());
        }
        else
        {
            Debug.LogError("No TextMeshProUGUI encontrado en este GameObject.");
        }
    }

    IEnumerator textoParpadeo()
    {
        while (estaParpadeando)
        {
            // Transición de opacidad hacia 0
            float tiempoDeshacer = 0f;
            while (tiempoDeshacer < transicionDuracion)
            {
                float alpha = Mathf.Lerp(1f, 0f, tiempoDeshacer / transicionDuracion);
                textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                tiempoDeshacer += Time.deltaTime;
                yield return null;
            }
                                                                                                                    //transición parpadeo para que no sea tan brusca 
            // Transición de opacidad hacia 1
            tiempoDeshacer = 0f;
            while (tiempoDeshacer < transicionDuracion)
            {
                float alpha = Mathf.Lerp(0f, 1f, tiempoDeshacer / transicionDuracion);
                textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                tiempoDeshacer += Time.deltaTime;
                yield return null;
            }
        }
    }

   
    public void PararParpadeo()
    {
        estaParpadeando = false;
        textMeshPro.color = originalColor; 
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