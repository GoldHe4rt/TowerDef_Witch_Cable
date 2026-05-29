using System;
using UnityEngine;

using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private MultiplayerScreenManager multiplayerScreenManager;
    public PlayerInput[] players; // assign in inspector
    [SerializeField] private bool CanJoin = false;
    public int activePlayerAmount = 0;

    private Dictionary<Gamepad, int> gamepadToPlayer = new Dictionary<Gamepad, int>();
    private HashSet<int> takenSlots = new HashSet<int>();
    
    // Static dictionary to persist controller assignments across scenes
    private static Dictionary<int, int> savedGamepadAssignments = new Dictionary<int, int>();
    
    
    private void Awake()
    {
        // Restore saved controller assignments from previous scenes
        RestoreSavedControllers();
        
        // Subscribe to scene changes to restore controllers
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Restore controller assignments in the new scene
        RestoreSavedControllers();
    }
    
    private void RestoreSavedControllers()
    {
        gamepadToPlayer.Clear();
        takenSlots.Clear();
        activePlayerAmount = 0;
        
        // Reassign saved gamepads to their player slots
        foreach (var gamepad in Gamepad.all)
        {
            int deviceId = gamepad.deviceId;
            if (savedGamepadAssignments.ContainsKey(deviceId))
            {
                int playerIndex = savedGamepadAssignments[deviceId];
                if (playerIndex < players.Length && players[playerIndex] != null)
                {
                    AssignGamepadToPlayer(gamepad, playerIndex);
                }
            }
        }
    }
    
    private void AssignGamepadToPlayer(Gamepad gamepad, int playerIndex)
    {
        var player = players[playerIndex];
        
        player.enabled = true;
        activePlayerAmount++;
        multiplayerScreenManager.playerData[playerIndex].isActive = true;
        if (multiplayerScreenManager != null)
            multiplayerScreenManager.UpdatePlayerAmount();
        
        player.SwitchCurrentControlScheme(gamepad);
        
        gamepadToPlayer.Add(gamepad, playerIndex);
        takenSlots.Add(playerIndex);
        
        // Save the assignment for future scenes
        savedGamepadAssignments[gamepad.deviceId] = playerIndex;
        
        Debug.Log($"Player {playerIndex + 1} joined. Active players: {activePlayerAmount}");
    }
    
    
    private void Update()
    {
        JoinGamepad();
    }

    void JoinGamepad()
    {
        if (!CanJoin) return;
        foreach (var gamepad in Gamepad.all)
        {
            // JOIN (A button)
            if (!gamepadToPlayer.ContainsKey(gamepad) &&
                gamepad.yButton.wasPressedThisFrame)
            {
                TryJoin(gamepad);
            }

            // LEAVE (B button)
            if (gamepadToPlayer.ContainsKey(gamepad) &&
                gamepad.bButton.wasPressedThisFrame)
            {
                Leave(gamepad);
            }
        }
    }


    void TryJoin(Gamepad gamepad)
    {
        int freeIndex = GetFreePlayerIndex();
        if (freeIndex == -1) return;

        AssignGamepadToPlayer(gamepad, freeIndex);
    }

    void Leave(Gamepad gamepad)
    {
        int index = gamepadToPlayer[gamepad];
        var player = players[index];

        // Disable input (or whole player if you want)
        player.enabled = false;
        activePlayerAmount--;
        multiplayerScreenManager.playerData[index].isActive = false;
        if (multiplayerScreenManager != null)
            multiplayerScreenManager.UpdatePlayerAmount();

        gamepadToPlayer.Remove(gamepad);
        takenSlots.Remove(index);
        
        // Remove from saved assignments
        savedGamepadAssignments.Remove(gamepad.deviceId);

        Debug.Log($"Player {index + 1} left. Active players: {activePlayerAmount}");

    }

    int GetFreePlayerIndex()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (!takenSlots.Contains(i))
                return i;
        }
        return -1;
    }
}
