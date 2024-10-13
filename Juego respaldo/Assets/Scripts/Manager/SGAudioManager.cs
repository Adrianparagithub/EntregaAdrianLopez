using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGAudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip menuMusic; // El clip de audio que quieres usar

    public AudioSource hoverAudioSource; // Fuente de audio para el sonido de hover
    public AudioSource clickAudioSource; // Fuente de audio para el sonido de clic

    void Awake()
    {
        // Asegúrate de que solo haya un AudioManager para evitar duplicados
        if (FindObjectsOfType<SGAudioManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); // No destruir este objeto al cambiar de escena
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = menuMusic;
        audioSource.loop = true; // Reproducir en bucle
        audioSource.Play(); // Comenzar a reproducir la música
    }

    public void PlayHoverSound()
    {
        if (hoverAudioSource != null)
        {
            hoverAudioSource.Play();
        }
    }

    public void PlayClickSound()
    {
        if (clickAudioSource != null)
        {
            clickAudioSource.Play();
        }
    }
}
