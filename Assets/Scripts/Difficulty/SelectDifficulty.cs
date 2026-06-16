using UnityEngine;
using TMPro;

public class SelectDifficulty : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    private string difficultyKey;
    public GameDifficulty currentDifficulty;
    [SerializeField] private GameObject levelButtons;
    [SerializeField] private TextMeshProUGUI selectedDifficultyText;

    private void Start()
    { 
        currentDifficulty = dataManager.gameDifficulty;
        DisallowLevelSelection();
    }

    #region ChangeDifficulty
    public void ChangeToEasy() => OnDifficultyChanged(difficulty: GameDifficulty.Easy);
    public void ChangeToNormal() => OnDifficultyChanged(difficulty: GameDifficulty.Normal);
    public void ChangeToHard() => OnDifficultyChanged(difficulty: GameDifficulty.Hard);
    public void ChangeToEndless() => OnDifficultyChanged(difficulty: GameDifficulty.Endless);
    #endregion
    
    private void OnDifficultyChanged(GameDifficulty difficulty)
    {
        currentDifficulty = difficulty;
        dataManager.gameDifficulty = difficulty;
        SaveDifficulty();
        AllowLevelSelection();
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                selectedDifficultyText.text = "Easy";
                break;
            case GameDifficulty.Normal:
                selectedDifficultyText.text = "Normal";
                break;
            case GameDifficulty.Hard:
                selectedDifficultyText.text = "Hard";
                break;
            case GameDifficulty.Endless:
                selectedDifficultyText.text = "Endless";
                break;
        }
    }

    private void SaveDifficulty()
    {
        PlayerPrefs.SetString(difficultyKey, currentDifficulty.ToString());
        Debug.Log("Difficulty set to " + PlayerPrefs.GetString(difficultyKey));
        PlayerPrefs.Save();
    }
    
    private void AllowLevelSelection()
    { 
        levelButtons.SetActive(true); 
        }

    private void DisallowLevelSelection()
    { 
        levelButtons.SetActive(false); 
        selectedDifficultyText.text = "";
    }
}
