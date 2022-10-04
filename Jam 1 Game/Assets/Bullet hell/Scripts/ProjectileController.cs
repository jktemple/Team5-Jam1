using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(moveSpeed, 0 , 0) * moveSpeed * Time.deltaTime);
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);
        if(pos.x > 1 || pos.y > 1){
            Destroy(gameObject);
        }
    }
}
