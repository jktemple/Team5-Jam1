using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeAttack : MonoBehaviour
{
    StealthGaurdInfo info;
    
    void Start()
    {
        info = GetComponent<StealthGaurdInfo>();
    }

    void Update()
    {
        NavMesh.SamplePosition(StealthGameManager.instance.player.transform.position, out var hit, 20, NavMesh.AllAreas);
        info.navAgent.SetDestination(hit.position);
        if (Vector3.Distance(transform.position, StealthGameManager.instance.player.transform.position) <= 1) {
            StealthGameManager.instance.KilPlayer();
        }
    }
}
