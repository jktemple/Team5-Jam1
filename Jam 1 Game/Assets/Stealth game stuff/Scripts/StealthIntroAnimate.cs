using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthIntroAnimate : MonoBehaviour
{
    public float speed;
    public float distance;
    Vector3 destination;
    public float threshold;
    void Start()
    {
        destination = transform.position + transform.forward * distance;
        GetComponent<StealthPlayerController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= threshold) { enabled = false; }
        if (StealthGameManager.instance.paused) { return; }

        SteathAudioManager.instance.PlayHere(0, GetComponent<AudioSource>());
        transform.position += transform.forward * speed;
        if (Vector3.Distance(transform.position, destination) <= 0.5f){
            GetComponent<StealthPlayerController>().enabled = true;
            enabled = false;
            SteathAudioManager.instance.StopSoundHere(0, GetComponent<AudioSource>());
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(transform.position + transform.forward * distance, 0.5f);   
    }
}
