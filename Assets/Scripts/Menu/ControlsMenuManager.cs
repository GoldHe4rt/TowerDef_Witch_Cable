using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class ControlsMenuManager : MonoBehaviour
    {
        public Button controlsButton;
        public GameObject controlsMenuUi;
    

        private void Start()
        { controlsMenuUi.SetActive(false); }

        public void ShowControls()
        { controlsMenuUi.SetActive(true); }

        public void HideControls()
        { controlsMenuUi.SetActive(false); }
    }
}
