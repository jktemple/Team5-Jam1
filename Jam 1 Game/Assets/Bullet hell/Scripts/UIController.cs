using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{

    public Fadeout fader;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextScene(){
         fader.FadeToLevel("Bullet hell scene");
    }
}
