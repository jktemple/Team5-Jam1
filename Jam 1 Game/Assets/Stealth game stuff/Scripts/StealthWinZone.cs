using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthWinZone : MonoBehaviour
{
    public GameObject guards;
    public GameObject outfitSprite;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == StealthGameManager.instance.player.gameObject) {
            StealthGameManager.instance.DisplayText("Alright, I got the Suit! Now I just need some food", gameObject, Color.white);
            guards.SetActive(false);

            outfitSprite.SetActive(true);
        }
    }
    
}
