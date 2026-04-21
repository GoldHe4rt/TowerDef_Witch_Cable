using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//So that event system is not forgotten when putting SceneManager prefab in the scene. (Easy to forget & spend long time figuring out)
/*[RequireComponent(typeof(EventSystem))]*/
public class SceneController : MonoBehaviour
{

    //Used for START_BUTTON (might change name). Loads the player selection. No scene yet.
    //public void SelectPlayerScene() { SceneManager.LoadScene("Player_Select"); }

    //To be used after players have selected their characters. Loads level1.
    //Temporarily used to start game before we have player selection in place.
    public void StartGame()
    {
        Debug.Log("StartButton was pressed");
        SceneManager.LoadScene("Level 1");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("The button is working");
    }
    
    //Used for MAIN_MENU_BUTTON. Loads Main_menu Scene.
    public void MainMenu()
    {
        Debug.Log("MainMenuButton was pressed");
        SceneManager.LoadScene("Main_menu");
    }
    
    //Used for SETTINGS_BUTTON. Loads the settings scene. No scene yet.
    public void SettingsScene() { SceneManager.LoadScene("Settings"); }

    //Run this when EXIT_BUTTON
    public void Exit()
    {
        Debug.Log("ExitButton was pressed");
        Application.Quit();
    }
}
