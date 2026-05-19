using AudioScripts;
using UnityEngine;

namespace Menu
{
    public class GameOverController : MonoBehaviour
    {
        public GameObject gameOverUI;
        /*[SerializeField] private MusicPlayer musicPlayer;*/
   
        private void Start()
        { gameOverUI.SetActive(false); }

        //A loss function should call this.
        public void ShowGameOver()
        {
            /*musicPlayer.PlayLoseJingle();*/
            gameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
