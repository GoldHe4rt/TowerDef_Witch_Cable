using AudioScripts;
using UnityEngine;

namespace Menu
{
    public class GameOverController : MonoBehaviour
    {
        public GameObject gameOverUI;
        public GameObject statsUI;
        [Header("Defeat jingle")] [SerializeField]
        private string loseJingleEventId = "Lose_Jingle";

        private void Start()
        {
            gameOverUI.SetActive(false);
            statsUI.SetActive(false);
        }
        
        public void ShowGameOver()
        {
            PlayLoseJingle();
            gameOverUI.SetActive(true);
            statsUI.SetActive(true);
            Time.timeScale = 0f;
        }
        private void PlayLoseJingle() => AudioSystem.Play(loseJingleEventId);
    }
}
