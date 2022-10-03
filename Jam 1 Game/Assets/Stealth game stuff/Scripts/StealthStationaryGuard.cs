using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class StealthStationaryGuard : MonoBehaviour
{
    public bool setStationPos;
    public Vector3 StationPos;
    public Vector2 angleRange = new Vector2(0, 180);
    public float turnSpeed = 0.5f;

    bool turningUp;
    float currentAngle = 0;
    bool pathing;

    StealthGaurdInfo infoScript;

    private void Start() {
        transform.localEulerAngles = Vector3.zero;
        infoScript = GetComponent<StealthGaurdInfo>();
    }

    void Update()
    {
        if (setStationPos) {
            setStationPos = false;
            StationPos = transform.position;
        }

        if (!infoScript.suspicious && !infoScript.foundPlayer) {
            if (Vector3.Distance(transform.position, StationPos) >= 1) {
                infoScript.navAgent.SetDestination(StationPos);
                pathing = true;
            }
            else {
                if (pathing) {
                    transform.localEulerAngles = Vector3.zero;
                    pathing = false;
                }

                if (currentAngle > angleRange.x && !turningUp) {
                    currentAngle -= turnSpeed;
                    transform.localEulerAngles += new Vector3(0, -turnSpeed, 0);
                    if (currentAngle <= angleRange.x) {
                        turningUp = true;
                    }
                }
                if (currentAngle < angleRange.y && turningUp) {
                    currentAngle += turnSpeed;
                    transform.localEulerAngles += new Vector3(0, turnSpeed, 0);
                    if (currentAngle >= angleRange.y) {
                        turningUp = false;
                    }
                }
            }
        }
    }
}
