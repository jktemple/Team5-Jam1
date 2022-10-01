using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StealthInvestigate : MonoBehaviour
{
    public Vector3 pointOfInterest;
    EntityInfo info;
    [Tooltip("the amount of time this entity will wait at the point of interested before returning to normal behaviour")]
    public float investigateTime = 3;
    float investigateCounter;

    private void Start() {
        //make sure the point of interest in on the navMesh
        NavMesh.SamplePosition(pointOfInterest, out var hit, 30, NavMesh.AllAreas);
        pointOfInterest = hit.position;

        info = GetComponent<EntityInfo>();
        Reset();
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
            investigateCounter -= Time.deltaTime;
            if (investigateCounter <= 0) {
                info.EndSuspicion(true);
                Reset();
                enabled = false;
            }
        }
    }

    private void Reset() {
        investigateCounter = investigateTime;
    }
}
