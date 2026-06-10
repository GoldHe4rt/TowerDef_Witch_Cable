using UnityEngine;

namespace Menu
{
    public class PauseManager : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        public GameObject statsUI;
        public static bool isPaused;
        public static bool canPause;
        
        private void Start() 
        { 
            pauseMenuUI.SetActive(false); 
            statsUI.SetActive(false);
            isPaused = false;
            canPause = true;
            Time.timeScale = 1f;
        }

        //For testing & keyboard.
        private void Update()
        { if (Input.GetKeyDown(KeyCode.P)) TogglePause(); }

        public void TogglePause()
        {
            if (!isPaused && canPause)
                Pause();
            else Resume();
        }

        public void Resume()
        {
            isPaused = false;
            pauseMenuUI.SetActive(false);
            statsUI.SetActive(false);
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            isPaused = true;
            pauseMenuUI.SetActive(true);
            statsUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void ForcePauseWithoutMenu()
        {
            canPause = false;
            isPaused = false;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
