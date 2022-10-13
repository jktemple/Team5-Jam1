using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StealthMenuManager : MonoBehaviour
{
    public string stealthGameSceneName;
    public string mainMenuSceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            PlayGame();
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            Exit();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(stealthGameSceneName);
    }

    public void Exit()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
