using System.Collections;
using UnityEngine;
using TMPro;

public class escrituratexto : MonoBehaviour
{
    public TextMeshProUGUI textoAgradecimientos;
    public string textoCompleto;
    public float velocidadEscritura = 0.1f;

    void Start()
    {
     
        textoAgradecimientos.text = "";

       
        StartCoroutine(EscribirTexto());
    }

    IEnumerator EscribirTexto()
    {
     
        for (int i = 0; i < textoCompleto.Length; i++)
        {
           
            textoAgradecimientos.text += textoCompleto[i];

           
            yield return new WaitForSeconds(velocidadEscritura);       //velocidad escritura y texto inicio y completo definido en textMeshPro unity 
        }
    }
}
