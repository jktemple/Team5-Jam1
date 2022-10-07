using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StealthGameManager : MonoBehaviour
{
    public static StealthGameManager instance;

    [Range(0, 1)]
    public float tension = 0;
    public GameObject RedOverlay;
    public GameObject YellowOverlay;
    public List<GameObject> alertedGaurds = new List<GameObject>();
    public List<GameObject> susGaurds = new List<GameObject>();
    public StealthPlayerController player;
    public bool playerHiding = false;

    [Header("key bindings")]
    public KeyCode SneakKey = KeyCode.LeftShift;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    public TextMeshProUGUI bottomText;
    public Image stealthIcon;
    public Sprite openEyeIcon;
    public Sprite closedEyeIcon;

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
            tension = Mathf.Lerp(tension, 1f, 0.01f);
        }
        else if (susGaurds.Count > 0 && tension < 0.5f) {
            tension = Mathf.Lerp(tension, 0.5f, 0.01f);
        }
        else {
            tension = Mathf.Lerp(tension, 0.1f, 0.01f);
        }

        SteathAudioManager.instance.SetMusicVolume(tension * 0.1f);

        stealthIcon.sprite = player.crouching ? closedEyeIcon : openEyeIcon;

        if (alertedGaurds.Count > 0) {
            RedOverlay.SetActive(true);
        }
        else {
            RedOverlay.SetActive(false);
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
        bottomText.color = (Color) color;
    }

    public void HideText(GameObject source) {
        if (source != currentTextSource) { return; }
        bottomText.text = "";
        currentTextSource = null;
    }
}
