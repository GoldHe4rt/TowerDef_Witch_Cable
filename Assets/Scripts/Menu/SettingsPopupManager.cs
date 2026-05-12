using System;
using UnityEngine;

namespace Menu
{
    public class SettingsPopupManager : MonoBehaviour
    {
        public GameObject settingsMenu;

        private void Start()
        { HideSettings(); }

        public void ShowSettings()
        {
            settingsMenu.SetActive(true); 
            Debug.Log("Showing settings");
        }

        public void HideSettings()
        {
            settingsMenu.SetActive(false);
            Debug.Log("Hiding Settings");
        }
    }
}
