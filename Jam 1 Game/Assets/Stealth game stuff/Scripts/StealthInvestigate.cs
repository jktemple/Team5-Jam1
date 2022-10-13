using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class StealthInvestigate : MonoBehaviour
{
    public Vector3 pointOfInterest;
    StealthGaurdInfo info;
    [Tooltip("the amount of time this guard will wait at the point of interest before returning to normal behaviour")]
    public float investigateTime = 3;
    public float investigateLookSpeed = 0.5f;
    public float lookAroundAngleLim = 45;
    float investigateCounter;
    bool investigatingBox;

    public bool navigatingToPoint;
    public Vector3 validPointOfInterest;

    private void Start() {
        info = GetComponent<StealthGaurdInfo>();
        Reset();
    }

    private void OnEnable() {
        lookAroundAngle = 0;
        investigatingBox = false;
        validPointOfInterest = pointOfInterest;
    }

    void Update()
    {
        RecalculateValidPointOfInterest();

        if (info.foundPlayer) {
            return;
        }

        //path to point of interest. once there, wait for investigateCounter seconds before disabling this behaviour;
        if (Vector3.Distance(transform.position, validPointOfInterest) > 1f && info.navAgent.speed > 0) {
            navigatingToPoint = true;
            if ( !info.navAgent.SetDestination(validPointOfInterest)) {
                RecalculateValidPointOfInterest();
            }
        }
        else {
            navigatingToPoint = false;
            if (investigatingBox) {
                if (StealthGameManager.instance.playerHiding == true && (StealthGameManager.instance.player.transform.position.x == pointOfInterest.x && StealthGameManager.instance.player.transform.position.z == pointOfInterest.z)) {
                    StealthGameManager.instance.KilPlayer();
                }
            }
            LookAround();

            investigateCounter -= Time.deltaTime;
            if (investigateCounter <= 0) {
                info.EndSuspicion(true);
                enabled = false;
            }
        }
    }

    void RecalculateValidPointOfInterest()
    {
        NavMesh.SamplePosition(pointOfInterest, out var hit, 20, NavMesh.AllAreas);
        validPointOfInterest = hit.position;
    }

    public void InvestigateHidingPlace(Vector3 location) {
        print("investigating box");
        pointOfInterest = location;
        investigatingBox = true;
    }

    float lookAroundAngle;
    bool lookingUp = false;
    void LookAround() {
        if (lookAroundAngle <= lookAroundAngleLim && lookingUp) {
            lookAroundAngle += investigateLookSpeed;
            transform.Rotate(0, investigateLookSpeed, 0);
        }
        if (lookAroundAngle >= lookAroundAngleLim) {
            lookingUp = false;
        }
        if (lookAroundAngle >= -lookAroundAngleLim && !lookingUp) {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(validPointOfInterest, "target", allowScaling:default, UnityEngine.Color.blue);
    }
}
