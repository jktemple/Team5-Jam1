using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCameraController : MonoBehaviour
{
    public Vector2 zLimits;

    GameObject player;

    private void Start() {
        player = StealthGameManager.instance.player.gameObject;
    }

    void Update()
    {
        float playerZ = player.transform.position.z;
        float targetZ = Mathf.Min(zLimits.y, Mathf.Max(zLimits.x, playerZ));
        transform.position = Vector3.Lerp(transform.position, new Vector3(StealthGameManager.instance.player.transform.position.x, transform.position.y, targetZ), 0.025f);
    }
}
