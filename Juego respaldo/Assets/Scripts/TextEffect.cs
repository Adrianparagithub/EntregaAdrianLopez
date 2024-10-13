using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    public TMP_Text tmpText;

    public Color visibleColor =  Color.white;
    public float blinkSpeed = 1.0f;

    private Color transparentColor;
    private bool isBlinking = false;

    public float delayTime = 1.0f;
    void Start()
    {
        if (tmpText == null)
        {
            tmpText = GetComponent<TMP_Text>();
        }

        transparentColor = new Color(visibleColor.r, visibleColor.g, visibleColor.b, 0);
        tmpText.color = transparentColor;
        StartCoroutine(ShowTextAfterDelay());
    }

    IEnumerator ShowTextAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isBlinking = true;
    }

    void Update()
    {
        if (isBlinking) 
        {
            float lerp = Mathf.PingPong(Time.time * blinkSpeed, 1.0f);
            tmpText.color = Color.Lerp(transparentColor, visibleColor, lerp);
        }
    }
}
