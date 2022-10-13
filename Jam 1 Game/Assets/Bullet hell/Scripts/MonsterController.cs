using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

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
            PlayerPrefs.SetInt("level", 3); 
            SceneManager.LoadScene("Transition Scene");
        }
    }

    public void ApplyDamage(float damage){
        health = health - damage;
        Debug.Log("Damage applied: " + health);
    }

    

    
}
