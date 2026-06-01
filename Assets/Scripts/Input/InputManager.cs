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
    private bool isPlacing = false;
    private float isPlacingInputTimer = 0f;

    void Update()
    {
        if (isPlacing)
        {
            if (!isBuilding)
            {
                playerCombatSystem.Attack(false);
            }
            if (isPlacingInputTimer > 0)
            {
                isPlacingInputTimer -= Time.deltaTime;
            }
            if (isPlacingInputTimer <= 0)
            {
                if (isBuilding)
                {
                    playerBuildSystem.PlaceStructure();
                }
                
            }
            
        }
    }

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
        if (playerMovement.knockbackRunning || !playerMovement.movementEnabled)
        {
            isPlacing = false;
            return;
        }
        if (value.isPressed)
        {
            isPlacing = true;
            isPlacingInputTimer = 0.3f;
            if (isBuilding)
            {
                playerBuildSystem.PlaceStructure();
            }
            if (!isBuilding)
            {
                playerCombatSystem.Attack(true);
            }
        }
        else
        {
            isPlacing = false;

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
                playerCombatSystem.RemoveWeapon();
                isBuilding = true;
                Debug.Log("Starting build mode.");
            }
            else
            {
                dismantleMode = false;
                playerBuildSystem.StopPlacement();
                playerCombatSystem.NewWeapon(0);
                isBuilding = false;
                Debug.Log("Stopping build mode.");
            }
        }
    }

    public void OnDismantleToggle(InputValue value)
    {
        if (value.isPressed)
        {

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
                Debug.LogWarning("Pause manager is not assigned.");
                return;
            }
            Debug.Log("Pause!");
            globalReferanceManager.pauseManager.TogglePause();
        }
    }
}
