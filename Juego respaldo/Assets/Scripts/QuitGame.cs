using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    
    void Update()
    {
        // Verifica si se ha presionado la tecla espacio
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Cierra el juego en modo de compilación
            Application.Quit();

            // En el editor de Unity, puedes usar esta línea para detener la reproducción
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    }
}
