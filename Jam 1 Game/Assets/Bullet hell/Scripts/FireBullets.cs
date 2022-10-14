using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
     [SerializeField]
    private float waveSpeed = 1f;

    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    [SerializeField]
    private float spread = 0.05f;

    [SerializeField]
    private float switchTime = 3f;

    private Vector3 bulletMoveDirection;
    private float timer = 0;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireWave", 0f, waveSpeed);
    }

    private void FireWave()
    {
        float angleStep = (endAngle-startAngle)/bulletsAmount;
        float angle = startAngle;
        
        for(int i = 0; i < bulletsAmount; i++){
            float bulDirX = transform.position.x + Mathf.Sin((angle*Mathf.PI)/180);
            float bulDirY = transform.position.y + Mathf.Cos((angle*Mathf.PI)/180);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().setDirection(bulDir);
            
            angle += angleStep;
        }
    }

    private void FireCascade()
    {
        float upperbound = transform.position.y + bulletsAmount/2 * spread;
       
        for(int i = 0; i < bulletsAmount; i++){
            float bulDirX = transform.position.x + -1;
            float bulDirY = transform.position.y + 0;

            
            Vector3 bulPos = new Vector3(0, upperbound - i*spread, 0);
        
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position - bulPos;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().setDirection(bulDir);
            
        }
    }

    private void FireSpear() 
    {
        float upperbound = transform.position.y + bulletsAmount/2 * spread;
        float sidebound = transform.position.x;
       
        for(int i = 0; i < bulletsAmount; i++){
            float bulDirX = transform.position.x + -1;
            float bulDirY = transform.position.y + 0;

            
            Vector3 bulPos = new Vector3(sidebound-i*spread, upperbound-i*(spread-0.27f), 0);
        
            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position - bulPos;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<Bullet>().setDirection(bulDir);
                bul.GetComponent<Bullet>().setMoveSpeed(12);
            
        }
    }



    // Update is called once per frame
    void Update()
    {

        if(timer >= switchTime){
            CancelInvoke();
            timer = 0;
            float rand = Random.Range(0f,3f);
            if(rand > 2){
               InvokeRepeating("FireCascade", 0f, waveSpeed); 
            } else if(rand > 1){
                InvokeRepeating("FireSpear", 0f, waveSpeed);
            } else {
                InvokeRepeating("FireWave", 0f, waveSpeed);
            }
        }
        timer += Time.deltaTime;
    }
}
