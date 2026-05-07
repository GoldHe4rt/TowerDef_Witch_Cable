using System;
using GameConrollers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class CurrentSelectedUI : MonoBehaviour
    {
        private EventSystem myEventSystem;

        [Header("Scripts we get stuff from")]
        private SettingsPopupManager settingsPopup;
        private PauseManager pauseManager;
        private SceneController sceneController;

        public GameObject firstPauseObject;
        public GameObject firstSettingsObject;
        
        private void OnEnable()
        {
            myEventSystem = EventSystem.current;
            if (myEventSystem == null)
            { Debug.Log("No event system found"); }
            if (EventSystem.current.currentSelectedGameObject == null)
            { Debug.Log("No game object selected"); }
        }

        private void Update()
        {
           SetSelectedUI(uiElement: null);
        }

        public void SetSelectedUI(GameObject uiElement)
        {
            if (uiElement != null)
            {
                EventSystem.current.SetSelectedGameObject(uiElement);
            }
        }
       
    }
}
