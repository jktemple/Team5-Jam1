using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityInfo : MonoBehaviour
{
    public bool foundPlayer = false;
    public bool targetPlayer = true;

    private GameObject player;
    [HideInInspector] public NavMeshAgent navAgent;
    MeleeAttack meleeComponent;

    private void Awake() {
        navAgent = GetComponent<NavMeshAgent>();
        meleeComponent = GetComponent<MeleeAttack>();
        meleeComponent.enabled = false;
    }

    void Start()
    {
        player = FindObjectOfType<StealthPlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPlayer) {
            Physics.Raycast(transform.position, player.transform.position - transform.position, out var hit);

            if (hit.collider.gameObject == player) {
                if (!StealthGameManager.instance.AlertedGaurds.Contains(gameObject))
                StealthGameManager.instance.AlertedGaurds.Add(gameObject);
                foundPlayer = true;
            }
            else
            {
                foundPlayer = false;
                StealthGameManager.instance.AlertedGaurds.Remove(gameObject);
            }
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

    private void OnDrawGizmos() {
        Debug.DrawRay(transform.position, FindObjectOfType<StealthPlayerController>().transform.position - transform.position);
    }
}
