using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerFoodManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource source;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        Destroy(gameObject);
        print("food collected");
        RunnerGameManager.instance.foodCollected += 1;
        RunnerGameManager.instance.foodUICollect();
        RunnerSoundManager.instance.PlayHere(3, source);
    
    }

    //from here I would want to communicate something to the UI, either using a observer pattern, or a singleton


}
