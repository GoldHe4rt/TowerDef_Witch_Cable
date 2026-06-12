using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    private SceneManager sceneManager;
    [SerializeField] private SceneController sceneController;
    [SerializeField]private string levelSelectKey;
    [Header("Level names")]
    public string[] levelScenes = { "Level 1", "Level 2", "Level 3" };
    [Header("Selected level")]
    public string levelToLoad;

    private void Start()
    {
        GetSelectedLevel();
        levelToLoad = PlayerPrefs.GetString(levelSelectKey);
    }

    public void SetToLevel1() => SetSelectedLevel(0);
    public void SetToLevel2() => SetSelectedLevel(1); 
    public void SetToLevel3() => SetSelectedLevel(2);
    
    private void SetSelectedLevel(int selectionIndex)
    {
        levelToLoad = selectionIndex switch
        {
            0 => levelScenes[0],
            1 => levelScenes[1],
            2 => levelScenes[2],
            _ => levelToLoad
        };
        PlayerPrefs.SetString(levelSelectKey, levelToLoad);
        PlayerPrefs.Save();
        Debug.Log("Selected level " + levelToLoad);
    }
    
    private void GetSelectedLevel()
    { PlayerPrefs.GetString(levelSelectKey, levelToLoad); }

    public void LoadSelectedLevel()
    {
        GetSelectedLevel();
        SceneManager.LoadScene(levelToLoad);
    }
}
