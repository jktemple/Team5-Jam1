using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthTutorialBox : MonoBehaviour
{
    public string text;
    public bool hideText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            if (hideText) {
                StealthGameManager.instance.HideText(gameObject, true);
            }
            StealthGameManager.instance.DisplayText(text, gameObject);
        }
    }
}
