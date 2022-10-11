using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkateboardTiming : MonoBehaviour
{


    //detect collision T seconds in the future -> on input, activate skating || on failure to input, detect failure

    bool groundTouch;
    bool triggerTouch;


    //public bool 
    //when the offset collission is detected, activate a UI for indicating parry

    
    private void skateboardCheck()
    {
        bool pInput = Input.GetButtonDown("Fire3");

        if (pInput && triggerTouch)
        {
            Debug.Log("Success");
        }
    }


    //bool private triggerIndicator()




    // Start is called before the first frame update
    void Start()
    {
        groundTouch = false;
        triggerTouch = false;
    }

    void OnCollisionStay()
    {
        groundTouch = true;
    }

    void OnCollisionExit()
    {
        groundTouch = false;
    }

    void OnTriggerStay(Collider other)
    {
        triggerTouch = true;
        Debug.Log(other);

    }

    void OnTriggerExit()
    {
        triggerTouch = false;
    }

    // Update is called once per frame





    void Update()
    {
        skateboardCheck();
        Debug.Log(triggerTouch);
    }
}
