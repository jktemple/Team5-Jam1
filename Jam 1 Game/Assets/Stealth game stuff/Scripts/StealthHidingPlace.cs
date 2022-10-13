using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthHidingPlace : MonoBehaviour
{
    bool interactable;
    public string enterText = "Press e to hide";
    public string exitText = "Press e to leave";
    Vector3 ExitPos;
    public BoxCollider collision;
    public Vector3 playerHideOffset;

    void Update()
    {
        if (interactable) {
            if (Input.GetKeyDown(StealthGameManager.instance.interactKey)) {
                if (!StealthGameManager.instance.player.hiding) {
                    SteathAudioManager.instance.PlayGlobal(7);
                    StealthGameManager.instance.playerHiding = true;
                    collision.enabled = false;
                    ExitPos = StealthGameManager.instance.player.transform.position;
                    StealthGameManager.instance.player.Hide();
                    StealthGameManager.instance.player.transform.position = transform.position + playerHideOffset;
                    StealthGameManager.instance.DisplayText(exitText, gameObject);
                }
                else {
                    StealthGameManager.instance.playerHiding = false;
                    StealthGameManager.instance.player.UnHide();
                    StealthGameManager.instance.player.transform.position = ExitPos;
                    collision.enabled = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            StealthGameManager.instance.DisplayText(enterText, gameObject);
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            StealthGameManager.instance.HideText(gameObject);
            interactable = false;
        }
    }

}
