using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StealthGameManager : MonoBehaviour
{
    public static StealthGameManager instance;
    public bool pixelShader = true;

    [Range(0, 1)]
    public float tension = 0;
    public GameObject RedOverlay;
    public GameObject YellowOverlay;
    public List<GameObject> alertedGaurds = new List<GameObject>();
    public List<GameObject> susGaurds = new List<GameObject>();
    public StealthPlayerController player;
    public bool playerHiding = false;
    public GameObject brightLight;
    public bool resetCheckpoints;

    [Header("key bindings")]
    public KeyCode SneakKey = KeyCode.LeftShift;
    public KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    public TextMeshProUGUI bottomText;
    public Image stealthIcon;
    public Sprite openEyeIcon;
    public Sprite closedEyeIcon;
    public GameObject pauseParent;
    [HideInInspector] public bool paused;

    //UI
    GameObject currentTextSource;        //which gameobject generated the text that's currently on the screen - this is so that text can be removed and overritten as needed

    [Header("tutorial")]
    public bool tutorialCompleted;
    public Vector3 tutorialResetPoint;

    private void Awake() {
        instance = this;
    }

    public void MakeCheckpoint(Vector3 pos) {
        PlayerPrefs.SetFloat("respawnX", pos.x);
        PlayerPrefs.SetFloat("respawnY", pos.y);
        PlayerPrefs.SetFloat("respawnZ", pos.z);
    }

    private void Start() {

        player = FindObjectOfType<StealthPlayerController>();
        if (PlayerPrefs.GetFloat("respawnY") > -1) {
            player.transform.position = new Vector3(PlayerPrefs.GetFloat("respawnX"), PlayerPrefs.GetFloat("respawnY"), PlayerPrefs.GetFloat("respawnZ"));
        }
        else {
            print("x:" + PlayerPrefs.GetFloat("respawnX"));
        }

        HideText(gameObject, true);
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (resetCheckpoints) {
            resetCheckpoints = false;
            MakeCheckpoint(new Vector3(-1, -1, -1));
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }


        if (alertedGaurds.Count > 0) {
            tension = Mathf.Lerp(tension, 1f, 0.01f);
        }
        else if (susGaurds.Count > 0 && tension < 0.5f) {
            tension = Mathf.Lerp(tension, 0.5f, 0.01f);
        }
        else {
            tension = Mathf.Lerp(tension, 0.1f, 0.01f);
        }

        SteathAudioManager.instance.SetMusicVolume(tension);

        stealthIcon.sprite = player.crouching ? closedEyeIcon : openEyeIcon;

        if (alertedGaurds.Count > 0) {
            RedOverlay.SetActive(true);
        }
        else {
            RedOverlay.SetActive(false);
            YellowOverlay.SetActive(false);
        }
    }

    void TogglePause()
    {
        if (!paused) {
            pauseParent.SetActive(true);
            paused = true;
            Time.timeScale = 0;
        }
        else {
            pauseParent.SetActive(false);
            paused = false;
            Time.timeScale = 1;
        }   
    }

    public void TurnOffBrightLight() {
        brightLight.SetActive(false);
    }

    public int flickerCount = 4;
    public void FlickerBrightLight() {
        StartCoroutine(flickerLight());
    }

    private IEnumerator flickerLight() {
        brightLight.SetActive(false);
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        brightLight.SetActive(true);
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        flickerCount -= 1;
        if (flickerCount > 0) {
            StartCoroutine(flickerLight());
        }
        else {
            TurnOffBrightLight();
        }
    }

    public void FailTutorialStage() {
        player.transform.position = tutorialResetPoint;
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

    public void HideText(GameObject source, bool sourceOverride = false) {
        if (source != currentTextSource && sourceOverride == false) { return; }
        bottomText.text = "";
        currentTextSource = null;
    }
}
