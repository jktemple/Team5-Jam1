using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Door : MonoBehaviour
{
    public List<int> controllerLayers = new List<int>();

    public bool automatic = true;

    public bool setClose;
    public bool setOpen;
    public Vector3 closePos;
    public Vector3 openPos;

    private bool opening;
    private bool closing;
    bool closed = true;
    float closeTimer = 0;

    void Start()
    {
        Close();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!closed) {
            closeTimer -= Time.deltaTime;
            if (closeTimer <= 0) {
                Close();
            }
        }
        

        if (opening) {
            transform.position = Vector3.Lerp(transform.position, openPos, 0.025f);
            if (Vector3.Distance(transform.position, openPos) <= 0.01f) {
                opening = false;
                closed = false;
            }
        }
        if (closing) {
            transform.position = Vector3.Lerp(transform.position, closePos, 0.025f);
            if (Vector3.Distance(transform.position, closePos) <= 0.01f) {
                closing = false;
                closed = true;
            }
        }


        if (setClose) {
            setClose = false;
            closePos = transform.position;
        }
        if (setOpen) {
            setOpen = false;
            openPos = transform.position;
        }
    }

    void Open() {
        print("opening");
        opening = true;
    }

    void Close() {
        closing = true;
    }

    private void OnTriggerExit(Collider other) {
        print("left: " + other.gameObject.name);
        if (controllerLayers.Contains(other.gameObject.layer) && !closed) {
            Close();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (controllerLayers.Contains(other.gameObject.layer) && closed) {
            Open();
            closeTimer = 2;
        }
    }
}
