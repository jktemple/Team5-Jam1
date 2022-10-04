using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class patrol : MonoBehaviour
{
    public bool stopWhenFound = true;
    private StealthGaurdInfo infoScript;

    public bool setPosition;
    public List<Vector3> patrolPoints;
    private int index = 0;


    private NavMeshAgent navAgent;

    void Start()
    {
        infoScript = GetComponent<StealthGaurdInfo>();
        if (infoScript.navAgent != null) {
            navAgent = infoScript.navAgent;
        }
        LineRenderer lineR = GetComponent<LineRenderer>();
        if (lineR != null) {
            Vector3[] LineRPositions = new Vector3[lineR.positionCount];
            lineR.GetPositions(LineRPositions);
            patrolPoints.Clear();
            foreach (Vector3 pos in LineRPositions) {
                patrolPoints.Add(pos);
            }
            lineR.enabled = false;
        }
        lineR = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (infoScript.foundPlayer) {
            return;
        }
        else if (infoScript.suspicious) {
            navAgent.SetDestination(transform.position);
            return;
        }

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
        }
    }

    private void OnDrawGizmosSelected() {
        for (int i = 0; i < patrolPoints.Count; i++) {
            Gizmos.DrawSphere(patrolPoints[i], 0.1f);
            if (i < patrolPoints.Count - 1) {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
            }
            else {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[0]);
            }
        }
    }
}
