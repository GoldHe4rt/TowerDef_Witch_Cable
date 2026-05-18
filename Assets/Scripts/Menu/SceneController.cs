using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneController : MonoBehaviour
    {
        public Scene currentScene;
        
        private void Start()
        {
            Scene currentScene = SceneManager.GetActiveScene();
        }
        
        public void StartGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 1);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Used for NEXT_LEVEL_BUTTON in the Win Menu. Build index will need some changes for this to work properly.
        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    
        public void MainMenu()
        {
          SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        
        public void SettingsScene() 
        { SceneManager.LoadScene("Settings"); }
    
        public void Exit()
        { Application.Quit(); }
    }
}
