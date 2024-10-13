using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float doubleShotDuration = 10.0f;  // Duración del efecto de doble disparo
    public float speed = 1.0f;
    
    void Update()
    {
        // Mover el power-up hacia abajo
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Destruir el power-up si sale de la pantalla
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Activar el doble disparo en el jugador
            Player player = other.GetComponent<Player>();
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            
            if (player != null)
            {
                player.ActivateDoubleShot(doubleShotDuration);
            }

            if (audioManager != null)
            {
                audioManager.PlayPowerUpSound();  // Reproducir el sonido del power-up
            }
            else
            {
                Debug.LogWarning("No se encontró un AudioManager en la escena.");
            }

            // Destruir el power-up después de activarse
            Destroy(this.gameObject);
        }
    }

    
}
