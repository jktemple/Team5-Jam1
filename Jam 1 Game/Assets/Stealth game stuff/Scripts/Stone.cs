using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public float lifeTime = 5;
    public float soundRange = 5;

    public int stoneSoundID = 4;
    AudioSource source;

    private void Start() {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        SteathAudioManager.instance.PlayHere(stoneSoundID, source, restart:true);
        Collider[] listeners = Physics.OverlapSphere(transform.position, soundRange);

        foreach (Collider collider in listeners) {
            StealthGaurdInfo guardScript = collider.GetComponent<StealthGaurdInfo>();
            if (guardScript != null) {
                guardScript.BecomeSuspicious(transform.position, 3);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, soundRange);
    }
}
