using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector3 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    private void Fire(){
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
