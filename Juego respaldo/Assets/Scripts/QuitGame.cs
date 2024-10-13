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
            // Cierra el juego en modo de compilaci�n
            Application.Quit();

            // En el editor de Unity, puedes usar esta l�nea para detener la reproducci�n
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }
    }
}
