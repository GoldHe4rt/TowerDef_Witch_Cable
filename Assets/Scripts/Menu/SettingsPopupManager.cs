using UnityEngine;

namespace Menu
{
    public class SettingsPopupManager : MonoBehaviour
    {
        public GameObject settingsMenu;
        [SerializeField] private PauseManager pauseManager;

        private void Start()
        { settingsMenu.SetActive(false); }

        public void ShowSettings()
        {
            pauseManager.ForcePauseWithoutMenu();
            settingsMenu.SetActive(true);
        }

        public void HideSettings()
        {
            settingsMenu.SetActive(false);
            pauseManager.canPause = true;
            pauseManager.Pause();
        }
    }
}
