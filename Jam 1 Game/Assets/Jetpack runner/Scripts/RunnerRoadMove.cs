using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RunnerObjectMove : MonoBehaviour
{

    public Vector3 velocity = new Vector3(-1.0f, 0f, 0f);
    public bool reverse;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (reverse){

            transform.position = transform.position - (velocity * Time.deltaTime);


        }
        else { 
            transform.position = transform.position + (velocity * Time.deltaTime);
        }
    }
}
