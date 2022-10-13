using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
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


    //the vectors are simply added together to find the new vector for change


    public Animator animator;
    //I give the functions a jetpack acceleration, a gravitational acceleration, and a terminal velocity and a player input -> new vector at a capped magnitude
    //eventually I think I want to input a jerk to the acceleration, or edit the movement speed a public bezier curve


    void VelocityChange(float lift, float gravity)
    {

        if(getJump)
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
    }

    // Update is called once per frame
    void Update()
    {
        getJump = Input.GetButton("Jump");
        groundedPlayer = controller.isGrounded;
       // int groundHash = playerAnimator.StringToHash("isGrounded");
       // int jetpackHash = playerAnimator.StringToHash("isJetpack");
      //  int parryHash = playerAnimator.StringToHash("isParry");


        if (getJump)
        {
            animator.SetBool("IsGrounded", false);
            animator.SetBool("IsJetpack", true);


        }else if (groundedPlayer)
        {
            animator.SetTrigger("IsParry");
            animator.SetBool("IsJetpack", false);
            animator.SetBool("IsGrounded", true);




        }
 
       




        VelocityChange(jetpackLiftAccel, gravityAccel);



        controller.Move(playerVelocity * Time.deltaTime);
    }





}
