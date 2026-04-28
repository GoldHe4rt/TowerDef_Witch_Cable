using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerBuildSystem playerBuildSystem;
    [SerializeField] private PauseManager pauseManager;
    private bool isBuilding = false;


    public void OnMove(InputValue value)
    {
        playerMovement.moveInput = value.Get<Vector2>();
        //Debug.Log("Move!");
    }

    public void OnLook(InputValue value)
    {
        playerMovement.lookInput = value.Get<Vector2>();
        //Debug.Log("Look!");
    }

    public void OnPlace(InputValue value)
    {
        if (value.isPressed)
        {
            if (isBuilding)
            {
                playerBuildSystem.PlaceStructure();
                Debug.Log("Place!");
            }
        }
    }

    public void OnBuildToggle(InputValue value)
    {
        if (value.isPressed)
        {
            if (!isBuilding)
            {
                playerBuildSystem.StartPlacement(0);
                isBuilding = true;
                Debug.Log("Starting build mode.");
            }
            else
            {
                playerBuildSystem.StopPlacement();
                isBuilding = false;
                Debug.Log("Stopping build mode.");
            }
        }
    }

    public void OnSelectRight(InputValue value)
    {
        if (value.isPressed)
        {
            if (isBuilding)
            {
                playerBuildSystem.IncreaseObjectID();
                Debug.Log("Select Right!");
            }
        }
    }

    public void OnSelectLeft(InputValue value)
    {
        if (value.isPressed)
        {
            if (isBuilding)
            {
                playerBuildSystem.DecreaseObjectID();
                Debug.Log("Select Left!");
            }
        }
    }
    
    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Pause!");
            if (!pauseManager.isPaused)
                pauseManager.Pause();
            else pauseManager.Resume();
        }
    }
}
