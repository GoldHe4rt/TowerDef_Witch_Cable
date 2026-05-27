using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class SceneController : MonoBehaviour
    {

        public void Join()
        { SceneManager.LoadScene("PlayerSelect"); }

        public void Level1()
        { SceneManager.LoadScene("Level 1"); }

        public void Level2()
        { SceneManager.LoadScene("Marius"); }

        public void Restart()
        { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

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
