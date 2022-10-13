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
    public bool deleteWhenTriggered;
    public int playSound = -1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            if (playSound != -1) { SteathAudioManager.instance.PlayGlobal(playSound); playSound = -1; }
            if (CompleteTutorial) { StealthGameManager.instance.tutorialCompleted = true; }
            if (checkpoint) { StealthGameManager.instance.tutorialResetPoint = other.transform.position; }
            if (hideText) {StealthGameManager.instance.HideText(gameObject, true); }
            if (FlickerLight) { StealthGameManager.instance.FlickerBrightLight(); }
            StealthGameManager.instance.DisplayText(text, gameObject);
            if (deleteWhenTriggered) { Destroy(gameObject);  }

        }
    }
}
