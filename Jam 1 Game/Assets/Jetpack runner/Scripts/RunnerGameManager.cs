using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

using UnityEngine;

public class RunnerGameManager : MonoBehaviour
{
    public static RunnerGameManager instance;
    public GameObject playerObject;

    public int foodCollected = 0;
    public int maxFood;


    //player reset throwaways

    private bool resetState = false;

    //player death state
    public bool deathState = false;
    public Vector3 startPos = new Vector3(0f, -0.38f, 6.28f);
    public Vector3 fakeDeathPos = new Vector3(-1000f, -1038f, 1028f);



    //Camera data for camera reset on death

    public Vector3 CamPosOffset = new Vector3(0f, 1.2f, -10f);
    private Transform _target;
    private Vector3 _playerPos;
    public Vector3 _playerPosOffset;


    private void Awake()
    {
        instance = this;
        playerObject = GameObject.Find("RunnerPlayer");
    
    
    }


    
    //Class methods



    public void PlayerReset()
    {
        _target = GameObject.Find("TargetOffset").transform;
        _playerPos = GameObject.Find("RunnerPlayer").transform.position;
        _playerPos.y = _playerPosOffset.y;

        print("reset");
        deathState = false;
        resetState = true;
    }

    //Player death
    public void FailedGame()
    {
        print("death");
        deathState = true;
        
        //Destroy();
        //trigger UI
        //add death explosion

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (deathState == true)
        {
            playerObject.transform.position = fakeDeathPos;
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
       
    }
}
