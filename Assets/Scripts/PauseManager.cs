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
            Debug.Log("Pressed esc or p");
            //Checks if paused is true or false before resuming or pausing.
            if (isPaused)
            {
                Debug.Log("Going to resume");
                Resume();
            }
            else
            {
                Debug.Log("at else");
                Pause();
            }
        }
    }

    public void Resume()
    {
        Debug.Log("Resuming");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        Debug.Log("Pausing");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
