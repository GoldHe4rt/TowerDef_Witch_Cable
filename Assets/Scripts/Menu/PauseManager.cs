using UnityEngine;

namespace Menu
{
    public class PauseManager : MonoBehaviour
    {
        public GameObject pauseMenuUI;
        public bool isPaused;

        private CurrentSelectedUI selectedUI;
    
        //Pause is false and the menu does not show at start.
        private void Start() 
        { 
            pauseMenuUI.SetActive(false); isPaused = false; 
            Time.timeScale = 1f;
        }

        private void Update()
        {
            //Pause or Unpause game when ESC or P is pressed
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                //Checks if paused is true or false before resuming or pausing.
                if (isPaused)
                { Resume(); }
                else
                { Pause(); }
            }
        }

        public void TogglePause()
        {
            if (!isPaused)
                Pause();
            else Resume();
        }

        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
            selectedUI.SetSelectedUI(uiElement: selectedUI.firstPauseObject);
        }
    }
}
