using UnityEngine;

namespace Menu
{
    public class WinMenuManager : MonoBehaviour
    {
        public GameObject winMenu;
        public GameObject statsUI;
        [SerializeField] private JingleManager jingleManager;

        private void Start()
        {
            winMenu.SetActive(false);
            statsUI.SetActive(false);
        }
        
        public void Win()
        {
            jingleManager.PlayWinJingle();
            winMenu.SetActive(true);
            statsUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
