using UnityEngine;

namespace Menu
{
    public class WinMenuManager : MonoBehaviour
    {
        public GameObject winMenu;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        { winMenu.SetActive(false); }
        
        //Call this when the win condition is met. Shows Win Menu and sets the time to 0.
        public void Won()
        {
            winMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
