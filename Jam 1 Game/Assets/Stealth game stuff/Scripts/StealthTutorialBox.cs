using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthTutorialBox : MonoBehaviour
{
    public string text;
    public bool hideText;
    public bool FlickerLight;
    public bool checkpoint = true;
    public bool CompleteTutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {

            if (CompleteTutorial) { StealthGameManager.instance.tutorialCompleted = true; }
            if (checkpoint) { StealthGameManager.instance.tutorialResetPoint = other.transform.position; }
            if (hideText) {StealthGameManager.instance.HideText(gameObject, true); }
            if (FlickerLight) { StealthGameManager.instance.FlickerBrightLight(); }
            StealthGameManager.instance.DisplayText(text, gameObject);

        }
    }
}
