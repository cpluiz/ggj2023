using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
    public RectTransform mainMenu, instructions, credits, options;
    public void StartIntroVideo(){
        SceneManager.LoadScene("Intro");
    }
    public void BackToMainMenu(){
        ChangeActiveInterface("mainMenu");
    }
    public void ShowInstructions(){
        ChangeActiveInterface("instructions");
    }
    public void QuitGame(){
        Application.Quit();
    }
    private void ChangeActiveInterface(string interfaceName){
        mainMenu.gameObject.SetActive(interfaceName == "mainMenu");
        instructions.gameObject.SetActive(interfaceName == "instructions");
        credits.gameObject.SetActive(interfaceName == "credits");
        options.gameObject.SetActive(interfaceName == "options");
    }
}
