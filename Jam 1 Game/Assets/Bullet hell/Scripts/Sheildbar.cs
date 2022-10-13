using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sheildbar : MonoBehaviour
{

    public Slider slider;
    public BulletHellController player;


    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = player.startingSheilds;
        slider.value = player.startingSheilds;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.shields;
    }
}
