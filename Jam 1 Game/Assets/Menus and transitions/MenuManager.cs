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
    public GameObject levelSelectMenu;


    void Start()
    {
        PlayerPrefs.SetFloat("respawnX", -1);
        PlayerPrefs.SetFloat("respawnY", -1);
        PlayerPrefs.SetFloat("respawnZ", -1);

        PlayerPrefs.SetInt("level", 0);
        player.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            levelSelectMenu.SetActive(false);
        }
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void PlayCutscene1()
    {
        player.gameObject.SetActive(true);
        player.enabled = true;
        videoTexture.SetActive(true);

        player.Play();
        player.loopPointReached += StartGame;
    }

    void StartGame(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(transitionSceneName);
    }

    public void PlayLevel1()
    {
        PlayerPrefs.SetInt("level", 0);
        SceneManager.LoadScene(transitionSceneName);
    }

    public void PlayLevel2()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(transitionSceneName);

    }

    public void PlayLevel3()
    {
        PlayerPrefs.SetInt("level", 2);
        SceneManager.LoadScene(transitionSceneName);

    }
}