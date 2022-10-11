using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthInfoAnimate : MonoBehaviour
{
    public float speed;
    public float distance;
    Vector3 destination;
    void Start()
    {
        destination = transform.position + transform.forward * distance;
        GetComponent<StealthPlayerController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed;
        if (Vector3.Distance(transform.position, destination) <= 0.5f){
            GetComponent<StealthPlayerController>().enabled = true;
            enabled = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(transform.position + transform.forward * distance, 0.5f);   
    }
}