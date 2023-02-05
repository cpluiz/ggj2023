using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public void StartIntroVideo(){
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame(){
        Application.Quit();
    }
    
}