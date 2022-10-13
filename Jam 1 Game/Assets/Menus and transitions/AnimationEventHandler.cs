using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public TransitionManager manager;
    public void NextScene()
    {
        manager.NextScene(); 
    }
}
