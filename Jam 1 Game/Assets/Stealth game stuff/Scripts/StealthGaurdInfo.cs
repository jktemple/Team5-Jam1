using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

public class StealthGaurdInfo : MonoBehaviour
{
    public bool foundPlayer = false;
    public bool suspicious = false;
    public bool targetPlayer = true;
    public bool tutorial;

    [Header("alert settings")]
    public float eyeLevelOffset = 0.2f;
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
        if (StealthGameManager.instance.tutorialCompleted) {
            maxVisibilityDistance = 0;
        }

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
        //raycast to player, if uninturrupted and at the correct angle and distance, player has been spotted.
        Physics.Raycast(transform.position + new Vector3(0, eyeLevelOffset, 0), player.transform.position - transform.position, out var hit);
        float angleToPlayer = Vector3.Angle(player.transform.position - transform.position, transform.forward);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (hit.collider.gameObject == player && angleToPlayer <= VisiblityConeAngle && distanceToPlayer <= maxVisibilityDistance) {
            timeSeen += Time.deltaTime;

            if (timeSeen > 0) {
                BecomeSuspicious(hit.point);
            }
            if (timeSeen >= alertTime) {
                Alert();
            }
            forgetCounter = forgetTime;
        }
        else {
            if (foundPlayer && StealthGameManager.instance.playerHiding && investigateComponent != null) {
                investigateComponent.InvestigateHidingPlace(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            }
            timeSeen = 0;
            if (!investigateComponent.enabled || !investigateComponent.navigatingToPoint || foundPlayer) { 
                forgetCounter -= Time.deltaTime; 
            }
            if (forgetCounter <= 0 && foundPlayer) {
                ForgetPlayer();
            }
        }
    }

    void ForgetPlayer() {
        foundPlayer = false;
        suspicious = false;
        plumbob.GetComponent<MeshRenderer>().material = white;
        StealthGameManager.instance.alertedGaurds.Remove(gameObject);
        EnableRegularBehavior();
    }

    void Alert() {
        foundPlayer = true;
        
        EndSuspicion();
        plumbob.GetComponent<MeshRenderer>().material = red;

        if (tutorial) {
            StealthGameManager.instance.FailTutorialStage();
            return;
        }

        if (!StealthGameManager.instance.alertedGaurds.Contains(gameObject)) {
            StealthGameManager.instance.alertedGaurds.Add(gameObject);
        }
    }

    public void BecomeSuspicious(Vector3 pointOfInterest, float _forgetTime = -1) {
        if (foundPlayer) { return; }

        if (!StealthGameManager.instance.susGaurds.Contains(gameObject)) { StealthGameManager.instance.susGaurds.Add(gameObject); }
        plumbob.GetComponent<MeshRenderer>().material = yellow;
        if (_forgetTime > 0) { forgetCounter = _forgetTime;  }
        suspicious = true;

        //optional component behavior
        if (investigateComponent != null) {
            investigateComponent.enabled = true;
            investigateComponent.pointOfInterest = pointOfInterest;
        }
        DisableRegularBehavior();   
    }

    void DisableRegularBehavior()
    {
        if (GetComponent<patrol>() != null) { GetComponent<patrol>().enabled = false; }
        if (GetComponent<StealthStationaryGuard>() != null) { GetComponent<StealthStationaryGuard>().enabled = false; }
    }

    void EnableRegularBehavior()
    {
        print("enabled regular behavior");

        if (GetComponent<patrol>() != null) { GetComponent<patrol>().enabled = true; }
        if (GetComponent<StealthStationaryGuard>() != null) { GetComponent<StealthStationaryGuard>().enabled = true; }
        if (investigateComponent != null) { investigateComponent.enabled = false; }
    }

    //called to return guard to normal behavior. without certain components, it's called automatically after suspicionTime seconds. if those components are present, it's instead called from those scripts. example: stealthInevestigate.cs
    public void EndSuspicion(bool calledFromComponent = false) {
        
        print("suspicion ended!");
        
        StealthGameManager.instance.susGaurds.Remove(gameObject);
        if (!suspicious || (!calledFromComponent && investigateComponent != null)) {
            return;
        }
        if (investigateComponent != null) {
            investigateComponent.enabled = false;
        }
        if (!foundPlayer) {
            EnableRegularBehavior();
        }
        plumbob.GetComponent<MeshRenderer>().material = white;
        suspicious = false;
        susCoutner = suspicionTime;
    }

    private void OnDrawGizmosSelected() {
        Debug.DrawRay(transform.position + new Vector3(0, eyeLevelOffset, 0), FindObjectOfType<StealthPlayerController>().transform.position - transform.position);
        Debug.DrawRay(transform.position, transform.forward);
        Gizmos.DrawWireSphere(transform.position, maxVisibilityDistance);
    }
}
