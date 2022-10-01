using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public bool crouching = false;
    public bool hiding = false;

    float horizontal;
    float vertical;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        if (hiding) {
            return;
        }
        if (Input.GetKey(StealthGameManager.instance.SneakKey) && !crouching) {
            crouching = true;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (Input.GetKeyUp(StealthGameManager.instance.SneakKey) && crouching) {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
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
