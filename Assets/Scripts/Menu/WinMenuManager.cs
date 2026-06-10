using UnityEngine;
using AudioScripts;

namespace Menu
{
    public class WinMenuManager : MonoBehaviour
    {
        public GameObject winMenu;
        public GameObject statsUI;
        [SerializeField] private MusicPlayer musicPlayer;

        private void Start()
        {
            winMenu.SetActive(false);
            statsUI.SetActive(false);
        }
        
        public void Win()
        {
            musicPlayer.PlayWinJingle();
            winMenu.SetActive(true);
            statsUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
