using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <=0){
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(float damage){
        health = health - damage;
        Debug.Log("Damage applied: " + health);
    }

    

    
}
