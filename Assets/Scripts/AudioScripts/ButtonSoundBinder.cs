using UnityEngine;
using UnityEngine.UI;

namespace AudioScripts
{
    public class ButtonSoundBinder : MonoBehaviour
    {
        [SerializeField] private string clickEventId = "UI_ButtonClick";

        private void Awake()
        {
            //Find all buttons in the scene (including inactive)
            var buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
            foreach (var button in buttons) AddSound(button);
        }

        private void AddSound(Button button)
        {
            //Prevent adding the sound twice
            button.onClick.RemoveListener(PlayClickSound);
            button.onClick.AddListener(PlayClickSound);
        }

        private void PlayClickSound() => AudioSystem.Play(clickEventId);
    }
}
