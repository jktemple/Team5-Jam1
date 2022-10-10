using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class StealthSlowBob : MonoBehaviour
{
    public bool setPos1;
    public bool setPos2;

    public Vector3 pos1;
    public Vector3 pos2;

    public float speed = 0.5f;

    bool goignUp = true;

    void Start()
    {
        
    }

    void Update()
    {
        if (setPos1) {
            setPos1 = false;
            pos1 = transform.position;
        }

        if (setPos2) {
            setPos2 = false;
            pos2 = transform.position;
        }

        if (!Application.isPlaying) {
            return;
        }

        if (goignUp) {
            transform.position += Vector3.up * speed; 

            //transform.position = Vector3.Lerp(transform.position, pos2, 0.025f, );

            if (transform.position.y >=  Mathf.Max(pos1.y, pos2.y)) {
                goignUp = false;
            }
        }
        else {
            transform.position -= Vector3.up * speed;

            //transform.position = Vector3.Lerp(transform.position, pos1, 0.025f);

            if (transform.position.y <= Mathf.Min(pos1.y, pos2.y)) {
                goignUp = true;
            }
        }
    }
}
