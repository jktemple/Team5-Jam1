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
    private Vector3 _playerPos;
    public Vector3 _playerPosOffset;
    void Start()
    {
        // 
        _target = GameObject.Find("TargetOffset").transform;
        _playerPos = GameObject.Find("RunnerPlayer").transform.position;
        _playerPos.y = _playerPosOffset.y;
    }
    // 
    void LateUpdate()
    {

        _target.position = (_playerPos + _playerPosOffset);
        // 
        this.transform.position = _target.
        TransformPoint(CamPosOffset);


        this.transform.LookAt(_target);
       // this.transform.rotation *= CamRotOffset;
    }
}
