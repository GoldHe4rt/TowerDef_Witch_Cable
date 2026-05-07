using System;
using UnityEngine;

namespace Menu
{
    public class SettingsPopupManager : MonoBehaviour
    {
        public GameObject settingsMenu;
        private CurrentSelectedUI selectedUI;

        private void Start()
        { HideSettings(); }

        public void ShowSettings()
        {
            settingsMenu.SetActive(true); 
            selectedUI.SetSelectedUI(uiElement : selectedUI.firstSettingsObject);
        }

        public void HideSettings()
        { settingsMenu.SetActive(false); }
    }
}
