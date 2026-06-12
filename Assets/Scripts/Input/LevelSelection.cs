using System;
using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    private SceneManager sceneManager;
    private string levelSelectKey = "SelectedLevel";
    private int defaultLevel = 1;
    
    public void SetToLevel1(int levelNumber) => SetSelectedLevel(levelNumber: 1);

    public void SetToLevel2(int levelNumber) => SetSelectedLevel(levelNumber: 2);

    public void SetToLevel3(int levelNumber) => SetSelectedLevel(levelNumber: 3);

    private void SetSelectedLevel(int levelNumber)
    {
        if (levelNumber < 1) Debug.Log("Cannot load a level");
        
        PlayerPrefs.SetInt("SelectedLevel", levelNumber);
        PlayerPrefs.Save();
        Debug.Log("Selected Level " + levelNumber);
    }

    private void LoadSelectedLevel()
    {
        int levelToLoad = GetLevel();
        string sceneName = $"Level{levelToLoad}";

        if (Application.CanStreamedLevelBeLoaded(sceneName))
            SceneManager.LoadScene(sceneName);
        else
            Debug.Log("Can't find the selected level");
    }

    private int GetLevel()
    { return PlayerPrefs.GetInt("SelectedLevel", defaultLevel); }
}
