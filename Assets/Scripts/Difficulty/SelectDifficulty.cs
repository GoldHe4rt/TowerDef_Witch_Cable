using UnityEngine;

public class SelectDifficulty : MonoBehaviour
{
    private DataManager dataManager;
    private string difficultyKey;
    public GameDifficulty currentDifficulty;
    [SerializeField] private GameObject levelButtons;

    private void Start()
    { levelButtons.SetActive(false); }

    #region ChangeDifficulty
    public void ChangeToEasy() => OnDifficultyChanged(difficulty: GameDifficulty.Easy);
    public void ChangeToNormal() => OnDifficultyChanged(difficulty: GameDifficulty.Normal);
    public void ChangeToHard() => OnDifficultyChanged(difficulty: GameDifficulty.Hard);
    public void ChangeToNightmare() => OnDifficultyChanged(difficulty: GameDifficulty.Nightmare);
    #endregion
    
    private void OnDifficultyChanged(GameDifficulty difficulty)
    {
        currentDifficulty = difficulty;
        SaveDifficulty();
        AllowLevelSelection();
    }

    private void SaveDifficulty()
    {
        PlayerPrefs.SetString(difficultyKey, currentDifficulty.ToString());
        Debug.Log("Difficulty set to " + PlayerPrefs.GetString(difficultyKey));
        PlayerPrefs.Save();
    }
    
    private void AllowLevelSelection()
    { levelButtons.SetActive(true); }
}
