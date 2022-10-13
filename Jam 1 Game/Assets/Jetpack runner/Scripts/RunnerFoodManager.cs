using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerFoodManager : MonoBehaviour
{
    // Start is called before the first frame update
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
    
    }

    //from here I would want to communicate something to the UI, either using a observer pattern, or a singleton


}
