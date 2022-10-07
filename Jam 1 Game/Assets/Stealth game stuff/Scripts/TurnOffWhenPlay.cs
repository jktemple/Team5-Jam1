using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffWhenPlay : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
}
