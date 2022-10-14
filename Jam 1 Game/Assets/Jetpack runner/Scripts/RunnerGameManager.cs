using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

using UnityEngine;
using System.Collections.Concurrent;

public class RunnerGameManager : MonoBehaviour
{
    public static RunnerGameManager instance;
    public GameObject playerObject;

    public GameObject canvas;
    private Transform transCanvas;
    public GameObject foodUI;


    public int foodCollected = 0;
    public int maxFood = 6;
    List<GameObject> foodUIs = new List<GameObject>();
    public GameObject[] foodObjects;
    public Vector3 foodOffset = new Vector3(60, 60, 0);
    public float sideOffset = 60f;
    //private Vector3 concurrentOffset = new Vector3(foodOffset);
    public Texture noFood;
    public Texture gotFood;

  




    public AudioSource death;
    private bool initial = true;
    //TextUI

    public GameObject instructionObj;
    public TextMeshProUGUI textMesh;


    //player death state
    public bool deathState = false;
    public Vector3 startPos = new Vector3(0f, -0.38f, 6.28f);
    public Vector3 fakeDeathPos = new Vector3(-1000f, -1038f, 1028f);

    public bool explosionState = false;


    public GameObject explosionObj;
    private ParticleSystem explosion;

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

    public void foodUICollect()
    {
        int foodElement = foodCollected - 1;
        GameObject passedObject = foodObjects[foodElement];
        RawImage passedRaw = passedObject.GetComponent<RawImage>();

        passedRaw.texture = gotFood;

    }

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
        explosionState = true;
        //Destroy();
        //trigger UI
        //add death explosion

    }

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;

        transCanvas = canvas.GetComponent<Transform>();
        playerObject = GameObject.Find("RunnerPlayer");
        textMesh = instructionObj.GetComponent<TextMeshProUGUI>();

        explosion = explosionObj.GetComponent<ParticleSystem>();


        foodUIs.Add(foodUI);
        for (int i = 0; i < maxFood-1; i++)
        {
            foodOffset.x += sideOffset;

            print("done!");
            GameObject newInstance = Instantiate(foodUI, transform);
            newInstance.transform.SetParent(transCanvas, false);

            newInstance.GetComponent<RectTransform>().anchoredPosition3D = foodOffset;

            foodUIs.Add(newInstance);

        }
        foodObjects = foodUIs.ToArray();


    }

    // Update is called once per frame
    void Update()
    {

        


         

        if(initial && Input.GetButtonDown("Jump"))
        {
            initial = false;
        }else if(initial && !deathState)
        {
            textMesh.text = "Press Space to Jump";
        }else if (!initial && !deathState)
        {
            textMesh.text = "";
        }
        
        if (explosionState)
        {
            explosionObj.transform.position = playerObject.transform.position;
            explosion.Play();
            RunnerSoundManager.instance.PlayHere(4, death);
            explosionState = false;
        }


        if (deathState == true)
        {
            textMesh.text = "Press Space to Reset";
            playerObject.transform.position = fakeDeathPos;
            
            
            
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }
       
    }
}
