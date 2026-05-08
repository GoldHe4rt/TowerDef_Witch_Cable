using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameConrollers
{
    public class SceneController : MonoBehaviour
    {
        public Scene currentScene;
        
        private void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();
        }

        //Loads level1.
        public void StartGame()
        {
            
            Debug.Log("StartButton was pressed");
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("The button is working");
        }
    
        public void MainMenu()
        {
            Debug.Log("MainMenuButton was pressed");
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
    
        //Used for SETTINGS_BUTTON. Loads the settings scene. No scene yet.
        public void SettingsScene() { SceneManager.LoadScene("Settings"); }
    
        public void Exit()
        {
            Debug.Log("ExitButton was pressed");
            Application.Quit();
        }
    }
}
