using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float fireSpeed;
    public GameObject bullet;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(-verticalInput,horizontalInput, 0) * moveSpeed * Time.deltaTime);
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        

        if(Input.GetAxis("Jump") > 0 && timer > fireSpeed){
            Debug.Log("Fire");
            timer = 0;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }
}
