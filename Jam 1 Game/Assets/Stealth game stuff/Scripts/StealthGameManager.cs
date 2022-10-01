using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StealthGameManager : MonoBehaviour
{
    public static StealthGameManager instance;
    public GameObject RedOverlay;
    public GameObject YellowOverlay;
    public List<GameObject> alertedGaurds = new List<GameObject>();
    public List<GameObject> susGaurds = new List<GameObject>();
    public StealthPlayerController player;

    [Header("key bindings")]
    public KeyCode SneakKey = KeyCode.LeftShift;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    public TextMeshProUGUI bottomText;

    //UI
    GameObject currentTextSource;        //which gameobject generated the text that's currently on the screen - this is so that text can be removed and overritten as needed

    private void Awake() {
        instance = this;
    }

    private void Start() {
        player = FindObjectOfType<StealthPlayerController>();
    }

    void Update()
    {
        if (alertedGaurds.Count > 0) {
            RedOverlay.SetActive(true);
        }
        else {
            RedOverlay.SetActive(false);
        }

        if (susGaurds.Count > 0) {
            YellowOverlay.SetActive(true);
        }
        else {
            YellowOverlay.SetActive(false);
        }
    }

    public void KilPlayer() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DisplayText(string text, GameObject source, Color ? color = null) {
        color = (color == null) ? Color.white : color;
        currentTextSource = source;
        bottomText.text = text;
    }

    public void HideText(GameObject source) {
        if (source != currentTextSource) { return; }
        bottomText.text = "";
        currentTextSource = null;
    }
}
