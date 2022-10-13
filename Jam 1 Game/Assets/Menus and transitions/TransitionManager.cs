using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public List<string> sceneNames = new List<string>();
    public RectTransform _parent;
    public GameObject character;
    public Image black;
    bool fadeIn;
    bool fadeOut;

    private void OnEnable() {
        black.gameObject.SetActive(true);
        fadeIn = true;
    }

    private void Update() {
        if (fadeIn) {
            character.gameObject.SetActive(false);
            black.color = Color.Lerp(black.color, new Color(0, 0, 0, 0), 0.025f);
            if (black.color.a <= 0.1f) {
                _parent.anchoredPosition = Vector3.right * (332 * PlayerPrefs.GetInt("level"));
                character.gameObject.SetActive(true);
                fadeIn = false;
            }
        }
        if (fadeOut) {
            character.gameObject.SetActive(false);
            black.color = Color.Lerp(black.color, new Color(0, 0, 0, 1), 0.025f);
            if (black.color.a >= 0.9f) {
                SceneManager.LoadScene(sceneNames[PlayerPrefs.GetInt("level")]);
                fadeOut = false;
            }
        }
    }

    public void NextScene()
    {
        fadeOut = true;
    }

}
