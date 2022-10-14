using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  UnityEngine.SceneManagement;

public class BulletHellController : MonoBehaviour
{
    public Healthbar healthbar;
    public Fadeout fader;
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
        healthbar.SetMaxHealth(health);
    }

    void FixedUpdate(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        shipRigidbody.MovePosition(transform.position + (new Vector3(horizontalInput, verticalInput, 0) * moveSpeed * Time.deltaTime));
       
        
    }

    void LateUpdate(){
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        pos.y = Mathf.Clamp(pos.y, 0.08f, 0.92f);
        pos.x = Mathf.Clamp(pos.x, 0.05f, 0.5f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    // Update is called once per frame
    void Update()
    {
        
       // transform.Translate(new Vector3(-verticalInput,horizontalInput, 0) * moveSpeed * Time.deltaTime);
        
        if(Input.GetAxis("Fire1") > 0 && shields > 0 && shieldCoolingdown == false){
            //Debug.Log("Shields Up");
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
            FindObjectOfType<AudioManager>().Play("Sword Swing");
        }
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }

    void takeDamage(float damage){
        if(isShielded){
            shields -= damage;
            FindObjectOfType<AudioManager>().Play("Sheild Clank");
            Debug.Log("Shields = " + shields);
        } else {
            health -= damage;
            Debug.Log("Health = " + health);
            healthbar.SetHealth(health);
        }

        if(health<= 0){
            Destroy(gameObject);
            fader.FadeToLevel("Bull Hell Menu");
        }
    }


}
