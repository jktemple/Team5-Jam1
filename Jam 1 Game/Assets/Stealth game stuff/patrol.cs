using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class patrol : MonoBehaviour
{
    public bool stopWhenFound = true;
    private EntityInfo infoScript;

    public bool setPosition;
    public List<Vector3> patrolPoints;
    private int index = 0;


    private NavMeshAgent navAgent;

    void Start()
    {
        infoScript = GetComponent<EntityInfo>();
        if (infoScript.navAgent != null) {
            navAgent = infoScript.navAgent;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (setPosition) {
            setPosition = false;
            NavMesh.SamplePosition(transform.position, out var hit, 30, NavMesh.AllAreas);
            patrolPoints.Add(hit.position);
        }

        if (!Application.isPlaying) {
            return;
        }

        navAgent.SetDestination(patrolPoints[index]);
        if (Vector3.Distance(transform.position, patrolPoints[index]) < 1f) {
            index += 1;
            if (index >= patrolPoints.Count) { index = 0; }
            print("reached destination");
        }
    }

    private void OnDrawGizmosSelected() {
        for (int i = 0; i < patrolPoints.Count; i++) {
            Gizmos.DrawSphere(patrolPoints[i], 0.1f);
            if (i < patrolPoints.Count + 1) {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
            }
        }
    }
}
