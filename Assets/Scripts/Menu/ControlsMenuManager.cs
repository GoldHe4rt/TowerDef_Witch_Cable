using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class ControlsMenuManager : MonoBehaviour
    {
        public Button controlsButton;
        public GameObject controlsMenuUi;
        public bool isActive;

        private void Start()
        { HideControls(); }

        public void ShowControls()
        { 
            controlsMenuUi.SetActive(true);
            isActive = true;
        }

        public void HideControls()
        { 
            controlsMenuUi.SetActive(false);
            isActive = false;
        }
    }
}
