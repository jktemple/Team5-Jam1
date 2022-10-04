using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCameraController : MonoBehaviour
{
    public Vector2 yLimits;

    GameObject player;

    private void Start() {
        player = StealthGameManager.instance.player.gameObject;
    }

    void Update()
    {
        float playerY = player.transform.position.y;
        float targetY = playerY > yLimits.y ? yLimits.y : playerY;
        transform.position = Vector3.Lerp(transform.position, new Vector3(StealthGameManager.instance.player.transform.position.x, transform.position.y, transform.position.z), 0.025f);
    }
}
