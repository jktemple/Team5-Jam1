using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halfWall : MonoBehaviour
{
    public BoxCollider fullCollider;
    bool nearby;

    void Update()
    {
        if (StealthGameManager.instance.player.crouching && nearby) {
            fullCollider.enabled = true;
        }
        else {
            fullCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) { nearby = true; }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) { nearby = false; }
    }
}
