using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntityInfo : MonoBehaviour
{
    bool foundPlayer = false;

    [HideInInspector] public NavMeshAgent navAgent;

    private void Awake() {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
