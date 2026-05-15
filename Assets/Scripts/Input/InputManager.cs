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
    [SerializeField] private PlayerCombatSystem playerCombatSystem;
    [SerializeField] private GlobalReferanceManager globalReferanceManager;
    private bool isBuilding = false;
    private bool dismantleMode = false;


    public void OnMove(InputValue value)
    {
        playerMovement.moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        playerMovement.lookInput = value.Get<Vector2>();
    }

    public void OnPlaceAttack(InputValue value)
    {
        if (value.isPressed)
        {
            if (isBuilding)
            {
                playerBuildSystem.PlaceStructure();
            }
            if (!isBuilding)
            {
                playerCombatSystem.Attack();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCombatSystem.Attack();
        }
    }

    public void OnBuildToggle(InputValue value)
    {
        if (value.isPressed)
        {
            if (globalReferanceManager.buildingEnabled == false)
            {
                Debug.LogError("Building is disabled.");
                return; 
            } 
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
                }
                else
                {
                    playerBuildSystem.StartPlacement(0);
                    dismantleMode = false;
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
                if (dismantleMode)
                {
                    dismantleMode = false;
                }
            } else
            {
                playerCombatSystem.IncreaseWeaponID();
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
                if (dismantleMode)
                {
                    dismantleMode = false;
                }
            }
            else
            {
                playerCombatSystem.DecreaseWeaponID();
            }
        }
    }

    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            if (globalReferanceManager.pauseManager == null)
            {
                Debug.LogError("Pause manager is not assigned.");
                return;
            }
            Debug.Log("Pause!");
            globalReferanceManager.pauseManager.TogglePause();
        }
    }

    public void OnShowControlls(InputValue value)
    {
        if (value.isPressed)
        {
            if (globalReferanceManager.controlsMenuManager == null)
            {
                Debug.LogError("Controls menu manager is not assigned.");
                return;
            }
            Debug.Log("ShowControlls!");
            globalReferanceManager.controlsMenuManager.ControlsToggle();
        }
    }
}
