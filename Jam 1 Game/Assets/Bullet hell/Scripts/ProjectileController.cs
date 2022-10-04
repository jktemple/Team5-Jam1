using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    private Rigidbody2D bulletRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        if(pos.x > 1 || pos.y > 1){
            Destroy(gameObject);
        }
    }

    void FixedUpdate(){
        bulletRigidbody.MovePosition(transform.position + new Vector3(moveSpeed, 0 , 0) * moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collider){
        //Debug.Log("Triggered");
        if(collider.tag == "Enemy"){
            Destroy(gameObject);
            collider.gameObject.SendMessage("ApplyDamage", damage);
        }
    }
}
