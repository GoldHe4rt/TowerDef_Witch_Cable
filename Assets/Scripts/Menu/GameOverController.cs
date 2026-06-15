using UnityEngine;

namespace Menu
{
    public class GameOverController : MonoBehaviour
    {
        public GameObject gameOverUI;
        public GameObject statsUI;
        [SerializeField] private JingleManager jingleManager;

        private void Start()
        {
            gameOverUI.SetActive(false);
            statsUI.SetActive(false);
        }
        
        public void ShowGameOver()
        {
            jingleManager.PlayLoseJingle();
            gameOverUI.SetActive(true);
            statsUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
