using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused;
    
    //Pause is false and the menu does not show at start.
    private void Start() 
    { pauseMenuUI.SetActive(false); isPaused = false; }

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

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
