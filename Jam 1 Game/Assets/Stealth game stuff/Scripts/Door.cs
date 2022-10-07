using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Door : MonoBehaviour
{
    public List<int> controllerLayers = new List<int>();
    public GameObject doorCollider;
    public bool close = false;
    public bool open = false;

    public bool automatic = true;

    public bool setClose;
    public bool setOpen;
    public Vector3 closePos;
    public Vector3 openPos;

    private bool opening;
    private bool closing;
    bool closed = true;
    float closeTimer = 0;

    public int soundID = 5;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        Close();   
    }

    // Update is called once per frame
    void Update()
    {
        if (open) {
            open = false;
            closeTimer = 2;
            Open();
        }
        if (close) {
            close = false;
            Close();
        }

        if (setClose) {
            setClose = false;
            closePos = doorCollider.transform.position;
        }
        if (setOpen) {
            setOpen = false;
            openPos = doorCollider.transform.position;
        }

        if (!Application.isPlaying) { return; }


        if (!closed) {
            closeTimer -= Time.deltaTime;
            if (closeTimer <= 0) {
                Close();
            }
        }

        if (opening) {
            SteathAudioManager.instance.PlayHere(soundID, source);
            doorCollider.transform.position = Vector3.Lerp(doorCollider.transform.position, openPos, 0.05f);
            if (Vector3.Distance(doorCollider.transform.position, openPos) <= 0.01f) {
                SteathAudioManager.instance.StopSoundHere(soundID, source);
                opening = false;
                closing = false;
                closed = false;
            }
        }
        if (closing) {
            SteathAudioManager.instance.PlayHere(soundID, source);
            doorCollider.transform.position = Vector3.Lerp(doorCollider.transform.position, closePos, 0.05f);
            if (Vector3.Distance(doorCollider.transform.position, closePos) <= 0.01f) {
                SteathAudioManager.instance.StopSoundHere(soundID, source);
                closing = false;
                opening = false;
                closed = true;
            }
        }


        
    }

    void Open() {
        print("opening");
        opening = true;
        closing = false;
    }

    void Close() {
        closing = true;
        opening = false;
    }

    private void OnTriggerExit(Collider other) {
        print("left: " + other.gameObject.name);
        if (controllerLayers.Contains(other.gameObject.layer) && !closed) {
            Close();
        }
    }

    private void OnTriggerStay(Collider other) {
        if (controllerLayers.Contains(other.gameObject.layer)) {
            Open();
            closeTimer = 2;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (controllerLayers.Contains(other.gameObject.layer) && closed) {
            Open();
            closeTimer = 2;
        }
    }
}
