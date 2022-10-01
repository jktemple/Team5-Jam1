using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class halfWall : MonoBehaviour
{
    BoxCollider collider;

    private void Start() {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StealthGameManager.instance.player.crouching) {
            collider.enabled = true;
        }
        else {
            collider.enabled = false;
        }
    }
}
