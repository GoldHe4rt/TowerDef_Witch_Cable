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
    [SerializeField] private GlobalInput globalInput;
    private bool isBuilding = false;
    private bool dismantleMode = false;


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
                dismantleMode = false;
                playerBuildSystem.StartPlacement(0);
                isBuilding = true;
                Debug.Log("Starting build mode.");
            }
            else
            {
                dismantleMode = false;
                playerBuildSystem.StopPlacement();
                isBuilding = false;
                Debug.Log("Stopping build mode.");
            }
        }
    }

    public void OnDismantleToggle(InputValue value)
    {
        if (value.isPressed)
        {
            if (isBuilding)
            {
                if (!dismantleMode)
                {
                    playerBuildSystem.StartRemoving();
                    dismantleMode = true;
                    Debug.Log("Starting dismantle mode.");
                }
                else
                {
                    playerBuildSystem.StartPlacement(0);
                    dismantleMode = false;
                    Debug.Log("Stopping dismantle mode.");
                }
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
                if (dismantleMode)
                {
                    dismantleMode = false;
                    Debug.Log("Stopping dismantle mode.");
                }
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
                if (dismantleMode)
                {
                    dismantleMode = false;
                    Debug.Log("Stopping dismantle mode.");
                }
            }
        }
    }

    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Pause!");
            globalInput.Pause();
        }
    }

    public void OnShowControlls(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("ShowControlls!");
            globalInput.ShowControlls();
        }
    }
}
