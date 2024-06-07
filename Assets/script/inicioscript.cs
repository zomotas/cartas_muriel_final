using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class inicioscript : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Buscar el VideoPlayer en el GameObject "cube"
        videoPlayer = GameObject.Find("Cube").GetComponent<VideoPlayer>();
        
        if (videoPlayer != null)
        {
            // Suscribirse al evento que se dispara cuando el video alcanza el final
            videoPlayer.loopPointReached += OnVideoEnd;

            // Si estamos en la escena "byemuriel", reproducir el video
            if (SceneManager.GetActiveScene().name == "byemuriel")
            {
                videoPlayer.Play();
            }
        }
        else
        {
            //Debug.LogError("VideoPlayer no encontrado en el GameObject 'cube'.");
        }
    }

    // Método que se llama cuando el video alcanza el final
    void OnVideoEnd(VideoPlayer vp)
    {
        // Verificar si estamos en la escena "byemuriel"
        if (SceneManager.GetActiveScene().name == "byemuriel")
        {
            // Cambiar a la escena "0menu"
            SceneManager.LoadScene("0menu");
        }
    }

    void Update()
    {
        // Verificar si se presiona la tecla Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Cargar la escena "0menu"
            SceneManager.LoadScene("0menu");
        }

        // Verificar si se presiona la tecla Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            if (currentSceneName == "cinematica")
            {
                SceneManager.LoadScene("1circo");
            }
            else if (currentSceneName == "carta2")
            {
                SceneManager.LoadScene("byemuriel");
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("videohistoria");
    }

    public void AgradecimientosGame()
    {
        SceneManager.LoadScene("agradecimientos");
    }

    public void ExitGame()
    {
        Debug.Log("salida");
        Application.Quit();
    }

    public void SuenaBoton()
    {
        AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.fxButton);
    }

    public void SuenaBeso()
    {
        AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.fxButtonAgradecimientos);
    } 
}