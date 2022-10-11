using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Vector3 moveDirection;
    private Rigidbody2D bulletRigidbody;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
       
    }

    void OnEnable(){
        Invoke("Destroy", 6f);
    }
    
    void FixedUpdate(){
        bulletRigidbody.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    public void setDirection(Vector3 dir){
        moveDirection = dir;
    }

    public void setMoveSpeed(float s){
        moveSpeed = s;
    }

    public void setDamage(float d){
        damage = d;
    }

    private void Destroy(){
        gameObject.SetActive(false);
    }

    private void OnDisable(){
        CancelInvoke();
    }
   private void OnTriggerEnter2D(Collider2D collider){
        //Debug.Log("Triggered");
        if(collider.tag == "Player"){
            //Debug.Log("collided with Player");
            collider.gameObject.SendMessage("takeDamage", damage);
            Destroy();
        }
    }
}
