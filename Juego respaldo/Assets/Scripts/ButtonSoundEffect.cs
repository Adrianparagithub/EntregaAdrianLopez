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

        // A�adir listeners para eventos
        button.onClick.AddListener(OnClick);
    }

    // M�todo llamado al pasar el mouse sobre el bot�n
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioManager?.PlayHoverSound();
    }

    // M�todo llamado al hacer clic en el bot�n
    private void OnClick()
    {
        audioManager?.PlayClickSound();
    }
}
