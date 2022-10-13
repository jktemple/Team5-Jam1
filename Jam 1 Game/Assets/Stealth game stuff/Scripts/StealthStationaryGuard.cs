using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

[ExecuteAlways]
public class StealthStationaryGuard : MonoBehaviour
{

    [Header("setup")]
    public bool setStationPos;
    public Vector3 StationPos;
    public Quaternion stationBaseQuat;
    public float stationBaseAngle;
    public float turnSpeed = 0.5f;
    public float LowerLim;
    public float upperLim;

    bool turningUp;
    public float currentAngle = 0;
    bool pathing;

    StealthGaurdInfo infoScript;

    private void Start() {
        transform.rotation = stationBaseQuat;
        currentAngle = stationBaseAngle;
        infoScript = GetComponent<StealthGaurdInfo>();
    }

    void Update()
    {
        

        if (setStationPos) {
            setStationPos = false;
            StationPos = transform.position;
            stationBaseQuat = transform.rotation;
        }

        if (infoScript.suspicious || infoScript.foundPlayer || !Application.isPlaying) {
            return;
        }

        if (StealthGameManager.instance.paused) { return; }

        if (!infoScript.suspicious && !infoScript.foundPlayer && Application.isPlaying) {
            if (Vector3.Distance(transform.position, StationPos) >= 1) {
                infoScript.navAgent.SetDestination(StationPos);
                pathing = true;
            }
            else {
                if (pathing) {
                    transform.rotation = stationBaseQuat;
                    transform.eulerAngles = new Vector3(0, stationBaseAngle, 0);
                    currentAngle = stationBaseAngle;
                    pathing = false;
                    infoScript.navAgent.isStopped = true;
                }

                if (currentAngle > (LowerLim) && !turningUp) {
                    currentAngle -= turnSpeed;
                    transform.localEulerAngles += new Vector3(0, -turnSpeed, 0);
                    if (currentAngle <= LowerLim) {
                        turningUp = true;
                    }
                }
                if (currentAngle < upperLim && turningUp) {
                    currentAngle += turnSpeed;
                    transform.localEulerAngles += new Vector3(0, turnSpeed, 0);
                    if (currentAngle >= upperLim) {
                        turningUp = false;
                    }
                }
            }
        }
    }
}
