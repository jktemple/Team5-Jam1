using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StealthGaurdInfo : MonoBehaviour
{
    public bool foundPlayer = false;
    public bool suspicious = false;
    public bool targetPlayer = true;

    [Header("alert settings")]
    public float VisiblityConeAngle = 45;
    public float maxVisibilityDistance = 30;
    [Tooltip("how many seconds must the player be visible before this enemey has 'found' the player, alerting other guards and attacking")]
    public float alertTime = 2;
    [Tooltip("how many seconds must the player be not seen after the guard was alerted before they forget they saw the player")]
    public float forgetTime = 2;
    float forgetCounter;
    [Tooltip("after catching a glimpse of the player, how many seconds before they lose insterest, if they don't see the player again")]
    public float suspicionTime = 2;
    float susCoutner;

    [Header("Pumbob")]
    public GameObject plumbob;
    public Material red;
    public Material yellow;
    public Material white;

    float timeSeen = 0;
    float timesuspicious = 0;

    private GameObject player;
    [HideInInspector] public NavMeshAgent navAgent;

    //optional components;
    MeleeAttack meleeComponent;
    StealthInvestigate investigateComponent;

    private void Awake() {
        navAgent = GetComponent<NavMeshAgent>();

        //components
        meleeComponent = GetComponent<MeleeAttack>();
        investigateComponent = GetComponent<StealthInvestigate>();
        meleeComponent.enabled = false;
        investigateComponent.enabled = false;
    }

    void Start()
    {
        player = FindObjectOfType<StealthPlayerController>().gameObject;
        susCoutner = suspicionTime;
    }


    void Update()
    {
        if (targetPlayer) {
            SearchForPlayer();
        }

        if (foundPlayer) {
            if (meleeComponent != null) {
                meleeComponent.enabled = true;
            }
        }
        else {
            if (meleeComponent != null) {
                meleeComponent.enabled = false;
            }
        }

    }

    void SearchForPlayer() {
        //decrement the time the entity has spent suspicious
        if (suspicious) {
            susCoutner -= Time.deltaTime;
            if (susCoutner <= 0) {
                EndSuspicion();
            }
        }

        //raycast to player, if uninturrupted and at the correct angle and distance, player has been spotted.
        Physics.Raycast(transform.position, player.transform.position - transform.position, out var hit);
        float angleToPlayer = Vector3.Angle(player.transform.position - transform.position, transform.forward);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (hit.collider.gameObject == player && angleToPlayer <= VisiblityConeAngle && distanceToPlayer <= maxVisibilityDistance) {
            timeSeen += Time.deltaTime;

            if (timeSeen > Time.deltaTime * 2) {
                BecomeSuspicious(hit.point);
            }
            if (timeSeen >= alertTime) {
                Alert();
            }
        }
        else {
            timeSeen = 0;
            forgetCounter -= Time.deltaTime;
            if (forgetCounter <= 0) {
                ForgetPlayer();
            }
        }
    }

    void ForgetPlayer() {
        foundPlayer = false;
        StealthGameManager.instance.alertedGaurds.Remove(gameObject);
    }

    void Alert() {
        EndSuspicion();
        plumbob.GetComponent<MeshRenderer>().material = red;
        if (!StealthGameManager.instance.alertedGaurds.Contains(gameObject)) {
            StealthGameManager.instance.alertedGaurds.Add(gameObject);
        }
        foundPlayer = true;
    }

    public void BecomeSuspicious(Vector3 pointOfInterest) {
        if (foundPlayer) { return;  }

        //if (!StealthGameManager.instance.susGaurds.Contains(gameObject)) { StealthGameManager.instance.susGaurds.Add(gameObject); }
        plumbob.GetComponent<MeshRenderer>().material = yellow;
        suspicious = true;

        //optional component behavior
        if (investigateComponent != null) {
            investigateComponent.enabled = true;
            investigateComponent.pointOfInterest = pointOfInterest;
        }
    }

    //called to return guard to normal behavior. without certain components, it's called automatically after suspicionTime seconds. if those components are present, it's instead called from those scripts. example: stealthInevestigate.cs
    public void EndSuspicion(bool calledFromComponent = false) {
        StealthGameManager.instance.susGaurds.Remove(gameObject);
        if (!suspicious || (!calledFromComponent && investigateComponent != null)) {
            return;
        }
        plumbob.GetComponent<MeshRenderer>().material = white;
        suspicious = false;
        susCoutner = suspicionTime;
    }

    private void OnDrawGizmosSelected() {
        Debug.DrawRay(transform.position, FindObjectOfType<StealthPlayerController>().transform.position - transform.position);
        Debug.DrawRay(transform.position, transform.forward);
        Gizmos.DrawWireSphere(transform.position, maxVisibilityDistance);
    }
}
