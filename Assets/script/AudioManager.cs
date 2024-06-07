using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public AudioClip bandaSonora;
    public AudioClip fxButton;
    public AudioClip fxButtonAgradecimientos;
    public AudioClip fxAgradecimientos;


    public GameObject musicObj;
    public GameObject player; // El personaje principal
    public List<GameObject> audioCharacters; // Lista de personajes con audio
    public float triggerDistance = 5.0f; // Distancia para activar el audio


    AudioSource _audioSource;
    AudioSource audioMusic;
    List<AudioSource> audioSourcesCharacters = new List<AudioSource>(); // Lista de AudioSources de los personajes


    public static AudioManager Instance;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    void Start()
    {
        _audioSource = this.GetComponent<AudioSource>();


        // Check if musicObj is assigned
        if (musicObj == null)
        {
            Debug.LogError("Music Obj is not assigned in the inspector!");
            return;
        }


        audioMusic = musicObj.GetComponent<AudioSource>();
        audioMusic.clip = bandaSonora;
        audioMusic.loop = true;
        audioMusic.volume = 0.2f;
        audioMusic.Play();


        foreach (var character in audioCharacters)
        {
            if (character != null)
            {
                AudioSource audioSource = character.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSourcesCharacters.Add(audioSource);
                }
            }
        }
    }


    void Update()
    {
        HandleProximityAudio();
    }


    void HandleProximityAudio()
    {
        if (player != null)
        {
            foreach (var audioSourceCharacter in audioSourcesCharacters)
            {
                float distance = Vector3.Distance(player.transform.position, audioSourceCharacter.transform.position);


                // Log the distance to the console
                Debug.Log($"Distance to {audioSourceCharacter.gameObject.name}: {distance}");


                if (distance <= triggerDistance)
                {
                    if (!audioSourceCharacter.isPlaying)
                    {
                        audioSourceCharacter.Play();
                        Debug.Log($"Playing audio for {audioSourceCharacter.gameObject.name}");
                    }
                }
                else
                {
                    if (audioSourceCharacter.isPlaying)
                    {
                        audioSourceCharacter.Stop();
                        Debug.Log($"Stopped audio for {audioSourceCharacter.gameObject.name}");
                    }
                }
            }
        }
    }


    public void CambiarClipAgradecimientos()
    {
        DetenerMusica();
        audioMusic.clip = fxAgradecimientos;
        audioMusic.loop = false; // Detener la reproducción en bucle
        audioMusic.volume = 1f; // Establecer el volumen al máximo
        audioMusic.Play();
    }


    public void ReanudarMusica()
    {
        audioMusic.clip = bandaSonora;
        audioMusic.loop = true;
        audioMusic.volume = 0.2f;
        audioMusic.Play();
    }


    public void DetenerMusica()
    {
        audioMusic.Stop();
    }


    public void SonarClipUnaVez(AudioClip ac)
    {
        _audioSource.PlayOneShot(ac);
    }
}


