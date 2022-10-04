using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCrouchWall : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            StealthGameManager.instance.player.crouching = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject && !Input.GetKey(StealthGameManager.instance.SneakKey)) {
            StealthGameManager.instance.player.crouching = false;
        }
    }
}
