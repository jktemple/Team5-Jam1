using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using UnityEngine;

public class RunnerCarHit : MonoBehaviour
{

    public Vector3 frontOffset= new Vector3(0, 0, 0);
    public float radius;
    public List<int> validLayers = new List<int>();
    [SerializeField] int carType;

    // Start is called before the first frame update
    void Start()
    {
    
        


    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + frontOffset, radius);

    }
    void Update()
    {

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position + frontOffset, radius);
        foreach (Collider collider in overlappedColliders)
        {
            if (validLayers.Contains(collider.gameObject.layer))
            {
                RunnerGameManager.instance.FailedGame();
                print("hit!");
            }
        }





    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RunnerSoundManager.instance.PlayGlobal(carType + 2, 1);

        }
    }
}
