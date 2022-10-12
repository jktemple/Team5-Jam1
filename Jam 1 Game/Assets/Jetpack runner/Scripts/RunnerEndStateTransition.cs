using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class RunnerEndStateTransition : MonoBehaviour
{

    public string transitionSceneName = "Transition Scene";

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("level", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter()
    {
        
        SceneManager.LoadScene(transitionSceneName);

        print("Transition");

    }
}
