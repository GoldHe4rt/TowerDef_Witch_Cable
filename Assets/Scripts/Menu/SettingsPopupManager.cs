using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class SettingsPopupManager : MonoBehaviour
    {
        [SerializeField]private PauseManager pauseManager;
        public GameObject settingsPopup;
        public Button settingsButton;
        public Button hideSettingsButton;

        private void Start()
        {
            JustHide();
            settingsButton.onClick.AddListener(ShowSettings);
            hideSettingsButton.onClick.AddListener(HideSettings);
        }

        //Use for the settings button
        public void ShowSettings()
        {
           settingsPopup.SetActive(true);
           pauseManager.ForcePauseWithoutMenu();
        }

        public void HideSettings()
        {
            settingsPopup.SetActive(false);
            pauseManager.canPause = true;
            pauseManager.isPaused = true;
            pauseManager.Pause();
        }

        private void JustHide()
        { settingsPopup.SetActive(false); }
    }
}
