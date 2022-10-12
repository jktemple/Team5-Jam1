using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;
using UnityEngine.AI;

[ExecuteAlways]
public class patrol : MonoBehaviour
{
    public enum patrolType {loop, bounceBack };
    public patrolType type;

    public bool stopWhenFound = true;
    private StealthGaurdInfo infoScript;

     bool setPosition;
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
                NavMesh.SamplePosition(pos, out var hit, 30, NavMesh.AllAreas);
                patrolPoints.Add(hit.position);
            }
            lineR.enabled = false;
        }
        lineR = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (setPosition) {
            setPosition = false;
            NavMesh.SamplePosition(transform.position, out var hit, 30, NavMesh.AllAreas);
            patrolPoints.Add(hit.position);
        }

        if (!Application.isPlaying || infoScript.foundPlayer || infoScript.suspicious) {
            return;
        }

        navAgent.SetDestination(patrolPoints[index]);
        if (Vector3.Distance(transform.position, patrolPoints[index]) < 1f) {
            index += 1;
            if (index >= patrolPoints.Count) { 
                if (type == patrolType.bounceBack) {
                    patrolPoints.Reverse();
                }
                index = 0; 
            }
        }
    }

    private void OnDrawGizmosSelected() {
        for (int i = 0; i < patrolPoints.Count; i++) {
            Gizmos.DrawSphere(patrolPoints[i], 0.1f);
            if (i < patrolPoints.Count - 1) {
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
            }
            else if (type == patrolType.loop){
                Gizmos.DrawLine(patrolPoints[i], patrolPoints[0]);
            }
        }
    }
}
