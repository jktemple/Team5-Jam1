using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            StealthGameManager.instance.MakeCheckpoint(other.transform.position);
        }
    }
}
