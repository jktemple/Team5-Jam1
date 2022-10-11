using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public string StealthGameSceneName = "Stealth game";
    public VideoPlayer player;
    public GameObject videoTexture;

    void Start()
    {
        PlayerPrefs.SetInt("level", 0);
    }

    void Update()
    {
        
    }

    public void PlayCutscene1()
    {
        videoTexture.SetActive(true);
        player.Play();
        //wait 9.5 seconds
        //SceneManager.LoadScene(StealthGameSceneName);
    }
}
