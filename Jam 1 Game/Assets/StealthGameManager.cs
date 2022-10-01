using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StealthGameManager : MonoBehaviour
{
    public static StealthGameManager instance;
    public GameObject RedOverlay;
    public List<GameObject> AlertedGaurds = new List<GameObject>();
    public StealthPlayerController player;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        player = FindObjectOfType<StealthPlayerController>();
    }

    void Update()
    {
        if (AlertedGaurds.Count > 0) {
            RedOverlay.SetActive(true);
        }
        else {
            RedOverlay.SetActive(false);
        }
    }

    public void KilPlayer() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
