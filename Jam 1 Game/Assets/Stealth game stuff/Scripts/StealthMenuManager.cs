using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StealthMenuManager : MonoBehaviour
{
    public string stealthGameSceneName;
    public string mainMenuSceneName;


    public void PlayGame()
    {
        SceneManager.LoadScene(stealthGameSceneName);
    }

    public void Exit()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
