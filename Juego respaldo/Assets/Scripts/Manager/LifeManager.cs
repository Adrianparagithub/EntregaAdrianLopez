using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LifeManager : MonoBehaviour
{
    public int lives = 4; // Vida inicial del jugador
    public GameObject lifeIconPrefab; // Prefab de la imagen de la nave
    public Transform livesPanel; // El panel donde colocarás los íconos
    private List<GameObject> lifeIcons = new List<GameObject>(); // Lista de los íconos de las vidas

    void Start()
    {
        // Inicializar las vidas al comienzo
        InitializeLives();
    }

    // Inicializa la UI con la cantidad de vidas actuales
    void InitializeLives()
    {
        for (int i = 0; i < lives; i++)
        {
            GameObject lifeIcon = Instantiate(lifeIconPrefab, livesPanel);
            lifeIcons.Add(lifeIcon);
        }
    }

    // Método para reducir la vida
    public void LoseLife()
    {
        if (lives > 0)
        {
            lives--;
            // Destruir el último icono de la lista
            Destroy(lifeIcons[lifeIcons.Count - 1]);
            lifeIcons.RemoveAt(lifeIcons.Count - 1);
        }

        if (lives <= 0)
        {
            // Lógica para manejar el fin del juego
            Debug.Log("Game Over");
        }
    }

    // Método para ganar una vida (opcional)
    public void GainLife()
    {
        lives++;
        GameObject lifeIcon = Instantiate(lifeIconPrefab, livesPanel);
        lifeIcons.Add(lifeIcon);
    }
}
