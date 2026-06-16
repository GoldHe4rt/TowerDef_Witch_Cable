using AudioScripts;
using UnityEngine;

namespace Menu
{
    public class WinMenuManager : MonoBehaviour
    {
        public GameObject winMenu;
        public GameObject statsUI;
        [Header("Victory Jingle")] 
        [SerializeField] private string winJingleEventId = "Win_Jingle";

        private void Start()
        {
            winMenu.SetActive(false);
            statsUI.SetActive(false);
        }
        
        public void Win()
        {
            PlayWinJingle();
            winMenu.SetActive(true);
            statsUI.SetActive(true);
            Time.timeScale = 0f;
        }

        private void PlayWinJingle() => AudioSystem.Play(winJingleEventId);
    }
}
