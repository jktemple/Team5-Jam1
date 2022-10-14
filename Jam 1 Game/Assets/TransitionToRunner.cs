using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToRunner : MonoBehaviour
{
    public void Transition()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene("Transition Scene");
    }
}
