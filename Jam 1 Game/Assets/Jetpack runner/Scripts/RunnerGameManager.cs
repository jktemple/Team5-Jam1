using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

using UnityEngine;
public class RunnerGameManager : MonoBehaviour
{
    public static RunnerGameManager instance;
    public GameObject playerObject;

    public GameObject canvas;
    public GameObject foodUI;

    public int foodCollected = 0;
    public int maxFood;
    List<GameObject> foodUIs = new List<GameObject>();

    public float foodOffset = 0f;
    //foodUIWidth = Canvas width/3
    //foodUIHeight = Canvas height/6

    //foodUIIncrement = foodImageWidth + _UIoffset

  /*  for (int i = 0; i < maxFood; i++)
    {
    



    }

    */

    //player reset throwaways


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
    
    
    }


    
    //Class methods



    public void PlayerReset()
    {
        print("reset");
        deathState = false;
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
        //Application.targetFrameRate = 60;
        playerObject = GameObject.Find("RunnerPlayer");


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
