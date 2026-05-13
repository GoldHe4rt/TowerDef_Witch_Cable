using UnityEngine;

namespace Menu
{
    public class PauseManager : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        public bool isPaused;
        public bool canPause;
        
        private void Start() 
        { 
            pauseMenuUI.SetActive(false); 
            isPaused = false;
            canPause = true;
            Time.timeScale = 1f;
        }

        //For testing on keyboard.
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
               TogglePause();
            }
        }

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
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            isPaused = true;
            pauseMenuUI.SetActive(true);
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
