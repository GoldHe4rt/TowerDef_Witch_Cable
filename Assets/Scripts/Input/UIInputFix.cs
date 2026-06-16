using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputForUI;
using UnityEngine.InputSystem.Users;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem.Utilities;

public class UIInputFix : MonoBehaviour
{
    [SerializeField] private ControllerManager controllerManager;
    private InputUser[] inputUsers;
    public EventSystem eventSystem;
    public InputDevice inputDevice;
    [SerializeField] private InputSystemUIInputModule uiInputModule;
    public InputAction navigate;
    public InputAction submit;
    public InputAction cancel;
    public InputAction point;
    public InputAction click;
    public static ReadOnlyArray<Gamepad> allGamepads { get; }
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        Debug.Log(string.Join("\n", Gamepad.all));
      navigate = uiInputModule.move;
      submit = uiInputModule.submit;
      cancel = uiInputModule.cancel;
      point = uiInputModule.point;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RandomBullshit()
    {
        var gamepad = allGamepads.Count;

        for (int i = 0; i < gamepad; i++)
        {
            InputActionChange.ActionMapEnabled.ToString("UI");
            
        }


    }
}
