using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public string StealthGameSceneName = "Stealth game";
    public string transitionSceneName = "Transition Scene";
    public VideoPlayer player;
    public GameObject videoTexture;

    bool playing;

    void Start()
    {
        PlayerPrefs.SetInt("level", 0);
    }

    public void PlayCutscene1()
    {
        videoTexture.SetActive(true);
        player.Play();
        player.loopPointReached += StartGame;
        //wait 9.5 seconds
        //SceneManager.LoadScene(StealthGameSceneName);
    }

    void StartGame(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(transitionSceneName);
    }
}




/*public class BubblePop : MonoBehaviour {


    private void Update()
    {
        if (Input.GetMouseButton(0)) {

        }
    }
}*/