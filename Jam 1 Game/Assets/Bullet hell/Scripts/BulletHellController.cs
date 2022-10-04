using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float fireSpeed;
    public float startingSheilds;
    public float shields;
    public float shieldDecay;
    public float shieldCooldownTimer;
    public float health;
    public GameObject bullet;
    float timer = 0;
    private Rigidbody2D shipRigidbody;
    private bool isShielded;
    private bool shieldCoolingdown = false;
    public GameObject sheildSprite;
    private SpriteRenderer shieldRenderer;
    public float cooldownTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        shipRigidbody = GetComponent<Rigidbody2D>();
        shieldRenderer = sheildSprite.GetComponent<SpriteRenderer>();
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
        
        if(Input.GetAxis("Fire1") > 0 && shields > 0 && shieldCoolingdown == false){
            Debug.Log("Shields Up");
            isShielded = true;
            shieldRenderer.enabled = true;
            shields = shields - shieldDecay*Time.deltaTime;
        } else {
            isShielded = false;
            shieldRenderer.enabled = false;
        }

        if(shields <= 0){
            shieldCoolingdown = true;
        }

        if(shieldCoolingdown == true){
            cooldownTimer+= Time.deltaTime;
        }

        if(cooldownTimer >= shieldCooldownTimer){
            cooldownTimer = 0;
            shields = startingSheilds;
            shieldCoolingdown = false;
        }

        if(Input.GetAxis("Jump") > 0 && timer > fireSpeed){
           // Debug.Log("Fire");
            timer = 0;
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }

    void takeDamage(float damage){
        if(isShielded){
            shields -= damage;
        } else {
            health -= damage;
        }
    }


}
