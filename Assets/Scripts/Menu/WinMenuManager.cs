using UnityEngine;
using AudioScripts;

namespace Menu
{
    public class WinMenuManager : MonoBehaviour
    {
        public GameObject winMenu;
        //[SerializeField] private MusicPlayer musicPlayer;
        private void Start()
        { winMenu.SetActive(false); }
        
        //Call this when the win condition is met. Shows Win Menu and sets the time to 0.
        public void Win()
        {
            //musicPlayer.PlayLoseJingle();
            winMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
