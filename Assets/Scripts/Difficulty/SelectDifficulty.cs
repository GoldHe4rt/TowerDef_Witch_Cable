using UnityEngine;

public class SelectDifficulty : MonoBehaviour
{
    private DataManager dataManager;
    public GameDifficulty CurrentDifficulty;
    [SerializeField] private GameObject levelButtons;

    private void Start()
    {
        levelButtons.SetActive(false);
    }

    #region ChangeDifficulty
    public void ChangeToEasy() => OnDifficultyChanged(difficulty: GameDifficulty.Easy);
    public void ChangeToNormal() => OnDifficultyChanged(difficulty: GameDifficulty.Normal);
    public void ChangeToHard() => OnDifficultyChanged(difficulty: GameDifficulty.Hard);
    public void ChangeToNightmare() => OnDifficultyChanged(difficulty: GameDifficulty.Nightmare);
    #endregion
    

    private void OnDifficultyChanged(GameDifficulty difficulty)
    {
        CurrentDifficulty = difficulty;
        AllowLevelSelection();
    }

    private void AllowLevelSelection()
    { levelButtons.SetActive(true); }
}
