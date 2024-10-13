using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int health = 1;
    public GameObject explosionPrefab;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Obtenemos la referencia de la c�mara principal
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckBounds();
    }

    public void Movement()
    {
        transform.Translate(new Vector3(Mathf.Sin(Time.time*1.5f), -1, 0) * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo destruido, activando explosi�n.");   
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Puedes ajustar el valor del da�o seg�n el juego
            TakeDamage(1);
        }
    }

    void CheckBounds()
    {
        Vector3 screenPosition = mainCamera.WorldToViewportPoint(transform.position);

        // Si el enemigo est� fuera de los l�mites de la pantalla, lo destruimos
        if (screenPosition.y < 0 || screenPosition.y > 1 || screenPosition.x < 0 || screenPosition.x > 1)
        {
            Destroy(gameObject);
        }
    }



}
