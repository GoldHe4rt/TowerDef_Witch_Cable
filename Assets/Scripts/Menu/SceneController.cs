using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private bool requirePlayers;
        [SerializeField] private int requiredPlayerCount = 1;
        [SerializeField] private ControllerManager _controllerManager;
        
        public bool EnoughPlayers()
        { 
            if (!requirePlayers) return true;
            return _controllerManager.activePlayerAmount >= requiredPlayerCount; 
        }

        public void Join()
        { SceneManager.LoadScene("PlayerSelect"); }

        public void ChooseLevel()
        { if (EnoughPlayers()) SceneManager.LoadScene("LevelSelect"); else Debug.Log("Not enough players"); }

        public void Tutorial()
        { if(EnoughPlayers()) SceneManager.LoadScene("Tutorial"); else Debug.Log("Not enough players"); }

        public void Level1()
        { if(EnoughPlayers()) SceneManager.LoadScene("Level 1"); else Debug.Log("Not enough players"); }

        public void Level2()
        { if(EnoughPlayers()) SceneManager.LoadScene("Marius"); else Debug.Log("Not enough players"); }

        public void Level3()
        { if(EnoughPlayers()) SceneManager.LoadScene("Level 3");else Debug.Log("Not enough players"); }

        public void Restart()
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

        //Used for NEXT_LEVEL_BUTTON in the Win Menu. Will need changes to make this work.
        public void NextLevel()
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
    
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
