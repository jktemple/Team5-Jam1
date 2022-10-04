using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float fireSpeed;
    public GameObject bullet;
    float timer = 0;
    private Rigidbody2D shipRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        shipRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        shipRigidbody.MovePosition(transform.position + (new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime));
        /*Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        shipRigidbody.MovePosition(Camera.main.ViewportToWorldPoint(pos));
        */
    }
    // Update is called once per frame
    void Update()
    {
        
       // transform.Translate(new Vector3(-verticalInput,horizontalInput, 0) * moveSpeed * Time.deltaTime);
        

        if(Input.GetAxis("Jump") > 0 && timer > fireSpeed){
           // Debug.Log("Fire");
            timer = 0;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }
}
