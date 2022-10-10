using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthWinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            StealthGameManager.instance.DisplayText("!!!!!!YOU WIN!!!!!!", gameObject, Color.yellow);
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
            Time.timeScale = 0;
        }
    }
}
