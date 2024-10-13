using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource powerUpAudioSource; // Fuente de audio para el sonido del power-up
    public AudioSource extraLifeAudioSource;
    public AudioSource menuMusicAudioSource;
    

    public void Start() // primer y unico fotograma 
    {
        PlayBackgroundMusic();
    }

    private void Update() //fotogtama a fotograma, desde el segundo hasta el ultimo 
    {
        
    }
    // Método para reproducir el sonido del power-up
    public void PlayPowerUpSound()
    {
        if (powerUpAudioSource != null)
        {
            powerUpAudioSource.Play();  // Reproduce el sonido asignado al AudioSource
        }
        else
        {
            Debug.LogWarning("No se ha asignado un AudioSource para el sonido del power-up.");
        }
    }

    public void PlayExtraLifeSound()
    {
        if (extraLifeAudioSource != null)
        {
            extraLifeAudioSource.Play();
        }
        
    }

    public void PlayBackgroundMusic()
    {
        if (menuMusicAudioSource != null && !menuMusicAudioSource.isPlaying)
        {
            menuMusicAudioSource.Play();
        }
    }
}
