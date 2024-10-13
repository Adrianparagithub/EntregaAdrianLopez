using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
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
            // Agregar una vida al jugador
            Player player = other.GetComponent<Player>();
            AudioManager audioManager = FindObjectOfType<AudioManager>();
            player.AddLife(); // Llama al método que agrega una vida
            
            if (audioManager != null)
            {
                audioManager.PlayExtraLifeSound(); // Reproduce el sonido de la vida extra
            }
            Destroy(gameObject); // Destruir la vida extra después de recogerla
        }
    }
}
