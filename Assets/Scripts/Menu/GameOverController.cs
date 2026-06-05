using AudioScripts;
using UnityEngine;

namespace Menu
{
    public class GameOverController : MonoBehaviour
    {
        public GameObject gameOverUI;
        public GameObject statsUI;
        [SerializeField] private MusicPlayer musicPlayer;

        private void Start()
        {
            gameOverUI.SetActive(false);
            statsUI.SetActive(false);
        }

        //A loss function should call this.
        public void ShowGameOver()
        {
            musicPlayer.PlayLoseJingle();
            gameOverUI.SetActive(true);
            statsUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
