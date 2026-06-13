using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private bool requirePlayers;
        [SerializeField] private int requiredPlayerCount = 1;
        [SerializeField] private ControllerManager _controllerManager;
        public int[] levelIndexes = { 4, 5, 6 };
        public int creditsIndex = 9;
        
        private bool EnoughPlayers()
        { 
            if (!requirePlayers) return true;
            return _controllerManager.activePlayerAmount >= requiredPlayerCount; 
        }
        public void Join()
        { SceneManager.LoadScene("PlayerSelect"); }

        public void ChooseLevel()
        { if (EnoughPlayers()) SceneManager.LoadScene("LevelSelect"); else Debug.Log("Not enough players"); }

        public void InfoScene()
        { SceneManager.LoadScene("Info"); }

        public void Credits()
        { SceneManager.LoadScene("Credits"); }

        public void Restart()
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

        //Used for NEXT_LEVEL_BUTTON in the Win Menu. Need to test to see if this works.
        public void NextLevel()
        {
            int current = SceneManager.GetActiveScene().buildIndex;

            for (int i = 0; i < levelIndexes.Length; i++)
                if (current == levelIndexes[i])
                {
                    SceneManager.LoadScene(i + 1 < levelIndexes.Length ? levelIndexes[i + 1] : creditsIndex);
                    return;
                }
        }
    
        public void MainMenu()
        { SceneManager.LoadScene(sceneBuildIndex: 0); }
        
        public void SettingsScene() 
        { SceneManager.LoadScene("Settings"); }

        public void ControlsScene()
        { SceneManager.LoadScene("Controls"); }
    
        public void Exit()
        { Application.Quit(); }
    }
}
