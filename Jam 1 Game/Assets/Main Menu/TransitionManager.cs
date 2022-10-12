using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public List<string> sceneNames = new List<string>();
    public RectTransform _parent;

    void Start()
    {
        print(332 * PlayerPrefs.GetInt("level") * Vector3.right);
        _parent.anchoredPosition = Vector3.right * (332 * PlayerPrefs.GetInt("level"));
    }

    public void NextScene()
    {
        SceneManager.LoadScene(sceneNames[PlayerPrefs.GetInt("level")]);
    }
}
