using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RunnerGameManager : MonoBehaviour
{
    public static RunnerGameManager instance;
    public GameObject playerObject;

    public int foodCollected = 0;
    public int maxFood;


    private void Awake()
    {
        instance = this;
        playerObject = GameObject.Find("RunnerPlayer");
    
    
    }


    //Player death

    public void FailedGame()
    {
        print("death");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
