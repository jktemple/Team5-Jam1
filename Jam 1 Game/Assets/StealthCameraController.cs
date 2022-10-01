using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(StealthGameManager.instance.player.transform.position.x, transform.position.y, transform.position.z), 0.025f);
    }
}
