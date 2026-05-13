using UnityEngine;

namespace Menu
{
    public class SettingsPopupManager : MonoBehaviour
    {
        public GameObject settingsMenu;
        private PauseManager pauseManager;
        public bool settingsActive;

        private void Start()
        {
            settingsMenu.SetActive(false);
            settingsActive = false;
        }

        public void ShowSettings()
        {
            Debug.Log("Settings button was pressed");
            settingsMenu.SetActive(true);
            settingsActive = false;
            pauseManager.ForcePauseWithoutMenu();
        }

        public void HideSettings()
        {
            settingsMenu.SetActive(false);
            settingsActive = false;
            pauseManager.canPause = true;
            pauseManager.Pause();
        }
    }
}
