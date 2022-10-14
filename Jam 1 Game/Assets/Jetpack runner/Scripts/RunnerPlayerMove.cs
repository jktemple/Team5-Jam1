using System;

using UnityEngine;

public class RunnerPlayerMove : MonoBehaviour
{
    private CharacterController controller;
    public Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool getJump;
    public float gravityAccel = -9.81f;

    public float jetpackJerk = 4.0f;
    public float jetpackLiftAccel = 12f;

    public float currentAccel = 0f;
    public float terminalVelocity = 80f;
    public float downwardCap = -30;
    //public float maxHeight;
    public float maxHeight = 100f;

    public Vector3 playerPos;

    //Sound source stuff
    public AudioSource source1;

    public AudioSource source2;

    //particles

    public GameObject jetpackObj;
    private ParticleSystem jetpack;
    public GameObject explosionObj;
    private ParticleSystem explosion;
    public GameObject skateboardObj;
    private ParticleSystem skateboard;

    //the vectors are simply added together to find the new vector for change
    public Animator animator;

    private bool parry = false;

    //I give the functions a jetpack acceleration, a gravitational acceleration, and a terminal velocity and a player input -> new vector at a capped magnitude
    //eventually I think I want to input a jerk to the acceleration, or edit the movement speed a public bezier curve


    void VelocityChange(float lift, float gravity)
    {

        if(getJump && (maxHeight > transform.position.y))
        {
            //RunnerSoundManager.instance.PlayGlobal(1);
            if (currentAccel < jetpackLiftAccel)
            {
                currentAccel += jetpackJerk;
            }
            playerVelocity.y += currentAccel;
        }else if(currentAccel > jetpackJerk)
        {
            currentAccel -= jetpackJerk/4;
        }

        if(!groundedPlayer)
        {
            playerVelocity.y += gravity;

        }
        


        Vector3 offset = offsetCalc(playerVelocity);
        playerVelocity = Vector3.ClampMagnitude(offset, terminalVelocity);
        playerVelocity.y = Mathf.Clamp(playerVelocity.y, downwardCap, 100);
    }

    Vector3 offsetCalc(Vector3 velocity)
    {
        Vector3 centerPt = transform.position; 
        Vector3 newPos = transform.position + velocity;

        Vector3 offset = newPos - centerPt;

        return offset;

    }


    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponent<Animator>();
        explosion = explosionObj.GetComponent<ParticleSystem>();
        skateboard = skateboardObj.GetComponent<ParticleSystem>();
        jetpack = jetpackObj.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        getJump = Input.GetButton("Jump");
        groundedPlayer = controller.isGrounded;
       
        playerPos = transform.position;






        if (getJump && groundedPlayer)
        {
            RunnerSoundManager.instance.PlayHere(2, source2);
        
        
        
        }

        if (!getJump)
        {
            RunnerSoundManager.instance.StopSoundHere(1, source1);
            jetpack.Stop();


        }

        if (getJump)
        {
            jetpack.Play();
            RunnerSoundManager.instance.StopSoundHere(2, source2);
            //set bool true
            RunnerSoundManager.instance.PlayHere(1, source1);
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsJetpack", true);
            parry = true;


        }
        else if (groundedPlayer)
        {
            //if bool audio, setbool to false,

            RunnerSoundManager.instance.StopSoundHere(1, source1);

            if (parry)
            {
               RunnerSoundManager.instance.PlayHere(2, source2);
               skateboard.Play();
               parry = false;
            }
           

            animator.SetTrigger("IsParry");
            animator.SetBool("IsJetpack", false);
            animator.SetBool("IsGrounded", true);
            





        }






        VelocityChange(jetpackLiftAccel, gravityAccel);



        controller.Move(playerVelocity * Time.deltaTime);
    }





}
