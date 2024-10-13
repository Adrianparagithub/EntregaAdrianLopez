using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSoundEffect : MonoBehaviour, IPointerEnterHandler
{
    private SGAudioManager audioManager; // Referencia al AudioManager

    private Button button; // Referencia al componente Button

    private void Start()
    {
        // Obtener el AudioManager en la escena
        audioManager = FindObjectOfType<SGAudioManager>();

        // Obtener el componente Button
        button = GetComponent<Button>();

        // Añadir listeners para eventos
        button.onClick.AddListener(OnClick);
    }

    // Método llamado al pasar el mouse sobre el botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager?.PlayHoverSound();
    }

    // Método llamado al hacer clic en el botón
    private void OnClick()
    {
        audioManager?.PlayClickSound();
    }
}
