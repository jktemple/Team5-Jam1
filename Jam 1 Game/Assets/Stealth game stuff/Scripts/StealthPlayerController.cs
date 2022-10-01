using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthPlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    public bool crouching = false;

    float horizontal;
    float vertical;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !crouching) {
            crouching = true;
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && crouching) {
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


    /*
    
    if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
            //rb.velocity += Vector3.forward * speed;
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(Vector3.back * speed * Time.deltaTime, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Force);
        }
    
    if (Input.GetKey(KeyCode.W) && rb.velocity.z <= speed) {
            rb.velocity += Vector3.forward * speed;
        }
        if (Input.GetKey(KeyCode.S) && rb.velocity.z >= -speed) {
            rb.velocity += Vector3.back * speed;
        }
        if (Input.GetKey(KeyCode.A) && rb.velocity.x >= -speed) {
            rb.velocity += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D) && rb.velocity.x <= speed) {
            rb.velocity += Vector3.right * speed;
        } 
     
     * 
     
     */
}
