using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class RunnerPlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool getJump;
    public float gravityAccel = -9.81f;

    public float jetpackLiftAccel = 12.0f;

    public float jetpackHorzAccel = 20f;
    public float terminalVelocity = 40f;
    //public float maxHeight;


    //the vectors are simply added together to find the new vector for change



    //I give the functions a jetpack acceleration, a gravitational acceleration, and a terminal velocity and a player input -> new vector at a capped magnitude
    //eventually I think I want to input a jerk to the acceleration, or edit the movement speed a public bezier curve


    void VelocityChange(float lift, float horzSpeed, float gravity)
    {

        if(getJump)
        {
            playerVelocity.y += lift;
        }

        if(!groundedPlayer)
        {
            playerVelocity.y += gravity;

        }
        playerVelocity.x += horzSpeed;


        Vector3 offset = offsetCalc(playerVelocity);
        playerVelocity = Vector3.ClampMagnitude(offset, terminalVelocity);

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
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        getJump = Input.GetButton("Jump");
        groundedPlayer = controller.isGrounded;
        
 
       




        VelocityChange(jetpackLiftAccel, jetpackHorzAccel, gravityAccel);



        controller.Move(playerVelocity * Time.deltaTime);
    }





}
