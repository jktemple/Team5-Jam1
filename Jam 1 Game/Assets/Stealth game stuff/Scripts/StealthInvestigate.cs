using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StealthInvestigate : MonoBehaviour
{
    public Vector3 pointOfInterest;
    StealthGaurdInfo info;
    [Tooltip("the amount of time this entity will wait at the point of interested before returning to normal behaviour")]
    public float investigateTime = 3;
    public float investigateLookSpeed = 0.5f;
    public float lookAroundAngleLim = 45;
    float investigateCounter;

    private void Start() {
        //make sure the point of interest in on the navMesh
        NavMesh.SamplePosition(pointOfInterest, out var hit, 30, NavMesh.AllAreas);
        pointOfInterest = hit.position;

        info = GetComponent<StealthGaurdInfo>();
        Reset();
    }

    private void OnEnable() {
        lookAroundAngle = 0;
    }

    void Update()
    {
        if (info.foundPlayer) {
            return;
        }

        //path to point of interest. once there, wait for investigateCounter seconds before disabling this behaviour;
        if (Vector3.Distance(transform.position, pointOfInterest) > 1) {
            info.navAgent.SetDestination(pointOfInterest);
        }
        else {
            LookAround();

            investigateCounter -= Time.deltaTime;
            if (investigateCounter <= 0) {
                info.EndSuspicion(true);
                Reset();
                enabled = false;
            }
        }
    }

    float lookAroundAngle;
    bool lookingUp = false;
    void LookAround() {
        if (lookAroundAngle <= lookAroundAngleLim && lookingUp) {
            print("looking up. currentAngle: " + lookAroundAngle);
            lookAroundAngle += investigateLookSpeed;
            transform.Rotate(0, investigateLookSpeed, 0);
        }
        if (lookAroundAngle >= lookAroundAngleLim) {
            lookingUp = false;
        }
        if (lookAroundAngle >= -lookAroundAngleLim && !lookingUp) {
            print("looking down. currentAngle: " + lookAroundAngle);
            lookAroundAngle -= investigateLookSpeed;
            transform.Rotate(0, -investigateLookSpeed, 0);
            
        }
        if (lookAroundAngle <= -lookAroundAngleLim) {
            lookingUp = true;
        }

    }

    private void Reset() {
        investigateCounter = investigateTime;
    }
}
