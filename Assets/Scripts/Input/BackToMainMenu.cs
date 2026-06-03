using System;
using Menu;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackToMainMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference backAction;
    [SerializeField]private SceneController sceneController;


    private void Update()
    {
        if (backAction.ToInputAction().triggered)
        {
            sceneController.MainMenu();
        }
    }
    
}
