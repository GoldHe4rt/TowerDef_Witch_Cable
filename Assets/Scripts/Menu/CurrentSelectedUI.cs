using System;
using GameConrollers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class CurrentSelectedUI : MonoBehaviour
    {
        private EventSystem myEventSystem;
        [SerializeField] private GameObject firstSelected;

        [Header("Scripts we get stuff from")]
        private SettingsPopupManager settingsPopup;
        private PauseManager pauseManager;
        private SceneController sceneController;

        private void Start()
        {
            if (myEventSystem == null)
            { Debug.Log("No event system found"); }
        }

        private void OnEnable()
        {
            myEventSystem = EventSystem.current;
        }

        private void Update()
        {
            //Following code in Update might be redundant
            //If isPaused set the currentSelected to be the first button in the pauseUI (figure out how to do that)
            if (pauseManager.isPaused)
            {
                
            }
            //If settingsPopUI is active then set the currentSelected to the first slider in the UI
            if (settingsPopup.settingsMenu)
            {
                
            }
        }

        private void FindFirstUIObject()
        {
            
        }
    }
}
