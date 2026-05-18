using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Menu
{
    public class ChangeButtonColor : MonoBehaviour
    {
        public Button myButton;
        private ColorBlock cb;
        private Image buttonImage;
        private SceneController sceneController;

        private void Start()
        {
            myButton.colors = ColorBlock.defaultColorBlock;
            cb = myButton.colors;
            buttonImage = myButton.image;
        }
        public void OnShowControls(InputAction.CallbackContext context)
        { 
            float inputValue = context.ReadValue<float>(); 
            Debug.Log(inputValue);
            if (inputValue >= 0.1f)
            {
                PressedColor();
            }
            else
            {
                NormalColor();
            }
        }

        public void BackToMainMenu(InputAction.CallbackContext context)
        {
            float inputValue = context.ReadValue<float>(); 
            Debug.Log(inputValue);
            if (inputValue >= 0.1f)
            {
                PressedColor();
                sceneController.MainMenu();
            }
            else
            {
                NormalColor();
            }
        }

        private void PressedColor()
        {
            buttonImage.color = cb.pressedColor;
        }

        private void NormalColor()
        {
            buttonImage.color = cb.normalColor;
        }
    }
}
