using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public List<string> sceneNames = new List<string>();
    public GameObject parent;

    void Start()
    {
        parent.transform.position = new Vector3(-830 * (PlayerPrefs.GetInt("level") * 332), parent.transform.position.y, parent.transform.position.z);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneNames[PlayerPrefs.GetInt("level")]);
    }
}
