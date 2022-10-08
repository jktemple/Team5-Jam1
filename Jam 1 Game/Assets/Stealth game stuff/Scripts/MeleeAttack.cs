using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour
{
    StealthGaurdInfo info;
    Vector3 lastKnownPos;

    void Start()
    {
        info = GetComponent<StealthGaurdInfo>();
    }

    void Update()
    {
        //if we can still see the player, update last known position
        Physics.Raycast(transform.position + new Vector3(0, info.eyeLevelOffset, 0), StealthGameManager.instance.player.transform.position - transform.position, out var raycasthit);
        if (raycasthit.collider.gameObject == StealthGameManager.instance.player.gameObject) { lastKnownPos = raycasthit.point;  }

        //navigate to last known position and murder if within range
        NavMesh.SamplePosition(lastKnownPos, out var hit, 20, NavMesh.AllAreas);
        info.navAgent.SetDestination(hit.position);
        if (Vector3.Distance(transform.position, StealthGameManager.instance.player.transform.position) <= 1) {
            StealthGameManager.instance.KilPlayer();
        }
    }
}
