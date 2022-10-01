using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public bool crouching = false;
    public bool hiding = false;

    public MeshRenderer crouchingVersion;
    MeshRenderer normalVersion;

    float horizontal;
    float vertical;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        normalVersion = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        normalVersion.enabled = !crouching;
        crouchingVersion.enabled = crouching;

        if (hiding) {
            return;
        }
        if (Input.GetKey(StealthGameManager.instance.SneakKey) && !crouching) {
            crouching = true;
        }
        else if (Input.GetKeyUp(StealthGameManager.instance.SneakKey) && crouching) {
            crouching = false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        float currentSpeed = crouching ? speed / 2 : speed;
        rb.velocity = new Vector3(horizontal * currentSpeed, 0, vertical * currentSpeed);
    }


    public void Hide() {
        hiding = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void UnHide() {
        hiding = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        GetComponent<BoxCollider>().enabled = true;
    }
}
