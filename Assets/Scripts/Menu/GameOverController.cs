using UnityEngine;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverUI;
   
    private void Start()
    { gameOverUI.SetActive(false); }

    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
