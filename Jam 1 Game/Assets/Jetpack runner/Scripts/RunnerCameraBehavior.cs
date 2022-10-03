using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCameraBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 CamPosOffset = new Vector3(0f, 1.2f, -10f);
    //public Quaternion CamRotOffset = new Quaternion(0, 0, 6, 0);
    // 
    private Transform _target;
    void Start()
    {
        // 
        _target = GameObject.Find("TargetOffset").transform;
        
    }
    // 
    void LateUpdate()
    {
        // 
        this.transform.position = _target.
        TransformPoint(CamPosOffset);


        this.transform.LookAt(_target);
       // this.transform.rotation *= CamRotOffset;
    }
}
