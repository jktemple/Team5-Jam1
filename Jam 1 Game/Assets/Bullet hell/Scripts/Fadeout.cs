using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fadeout : MonoBehaviour
{
      
    public Animator animator;
    private string level;

    public void FadeToLevel(string name){
        animator.SetTrigger("FadeOut");
        level = name;
    }

    public void OnFadeComplete(){
        SceneManager.LoadScene(level);
    }

}
