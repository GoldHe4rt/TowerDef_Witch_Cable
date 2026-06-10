using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;
using Menu;

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

    private int buildingIndex = 0;
    private int weaponIndex = 0;

    void Update()
    {
        if (playerMovement.knockbackRunning || !playerMovement.movementEnabled || PauseManager.isPaused)
            return;
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
        if (playerMovement.knockbackRunning || !playerMovement.movementEnabled || PauseManager.isPaused)
            return;
        playerMovement.moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (playerMovement.knockbackRunning || !playerMovement.movementEnabled || PauseManager.isPaused)
            return;
        
        playerMovement.lookInput = value.Get<Vector2>();
    }

    public void OnPlaceAttack(InputValue value)
    {
        if (playerMovement.knockbackRunning || !playerMovement.movementEnabled || PauseManager.isPaused)
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
        if (playerMovement.knockbackRunning || !playerMovement.movementEnabled || PauseManager.isPaused)
            return;
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
                weaponIndex = playerCombatSystem.currentWeaponID;
                playerBuildSystem.StartPlacement(buildingIndex);
                playerCombatSystem.RemoveWeapon();
                isBuilding = true;
                Debug.Log("Starting build mode.");
            }
            else
            {
                dismantleMode = false;
                buildingIndex = playerBuildSystem.selectedObjectID;
                playerBuildSystem.StopPlacement();
                playerCombatSystem.NewWeapon(weaponIndex);
                isBuilding = false;
                Debug.Log("Stopping build mode.");
            }
        }
    }

    public void OnSelectRight(InputValue value)
    {
        if (PauseManager.isPaused || playerMovement.LockRotation)
            return;
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
        if (PauseManager.isPaused || playerMovement.LockRotation)
            return;
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
