using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class MonsterController : MonoBehaviour
{
    public Fadeout fader;
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
            fader.FadeToLevel("Transition Scene");
        }
    }

    public void ApplyDamage(float damage){
        health = health - damage;
        Debug.Log("Damage applied: " + health);
        FindObjectOfType<AudioManager>().Play("Plant Hit");
    }

    public void Swing(){
        FindObjectOfType<AudioManager>().Play("Boss Swing");
    }

    

    
}
