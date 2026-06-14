using System;
using UnityEngine;

using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private MultiplayerScreenManager multiplayerScreenManager;
    [SerializeField] GlobalReferanceManager globalReferanceManager;
    public PlayerInput[] players; // assign in inspector
    [SerializeField] private bool CanJoin = false;
    public int activePlayerAmount = 0;

    private Dictionary<Gamepad, int> gamepadToPlayer = new Dictionary<Gamepad, int>();
    private HashSet<int> takenSlots = new HashSet<int>();
    
    // Static data to persist controller assignments and join order across scenes
    private static Dictionary<int, int> savedGamepadAssignments = new Dictionary<int, int>();
    private static List<int> savedGamepadOrder = new List<int>();
    
    private bool isRestoringControllers = false;
    
    
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
        activePlayerAmount = 0; // Reset active player count before restoring
        RestoreSavedControllers();
    }
    
    private void RestoreSavedControllers()
    {
        // Prevent multiple simultaneous restore operations
        if (isRestoringControllers)
            return;

        isRestoringControllers = true;
        gamepadToPlayer.Clear();
        takenSlots.Clear();
        activePlayerAmount = 0;

        // Build a map of device IDs to connected gamepads
        var connectedGamepads = new Dictionary<int, Gamepad>();
        foreach (var gamepad in Gamepad.all)
        {
            connectedGamepads[gamepad.deviceId] = gamepad;
        }

        // Build ordered list of (deviceId, playerIndex) for connected saved controllers
        var restoreList = new List<KeyValuePair<int,int>>();
        foreach (var deviceId in savedGamepadOrder)
        {
            if (!connectedGamepads.TryGetValue(deviceId, out var gamepad))
                continue;

            if (savedGamepadAssignments.TryGetValue(deviceId, out int playerIndex))
            {
                if (playerIndex < players.Length && players[playerIndex] != null)
                {
                    restoreList.Add(new KeyValuePair<int,int>(deviceId, playerIndex));
                }
            }
        }

        // Start coroutine to restore sequentially so PlayerInput binding happens in desired order
        if (restoreList.Count > 0)
            StartCoroutine(RestoreInOrder(restoreList));
        else
            isRestoringControllers = false;
    }

    private IEnumerator RestoreInOrder(List<KeyValuePair<int,int>> restoreList)
    {
        // Wait one frame to allow scene objects to fully initialize
        yield return null;

        // Build quick lookup for connected gamepads
        var connected = new Dictionary<int, Gamepad>();
        foreach (var g in Gamepad.all)
            connected[g.deviceId] = g;

        foreach (var kv in restoreList)
        {
            if (!connected.TryGetValue(kv.Key, out var gamepad))
                continue;

            // Assign without re-recording order to avoid duplicating entries
            AssignGamepadToPlayer(gamepad, kv.Value, false);

            // Allow a frame for Input System to settle between assignments
            yield return null;
        }

        // Restore complete
        isRestoringControllers = false;
    }
    
    private void AssignGamepadToPlayer(Gamepad gamepad, int playerIndex, bool recordOrder = true)
    {
        Debug.Log($"Assigning Gamepad {gamepad.deviceId} to Player {playerIndex + 1}");

        if (playerIndex < 0 || playerIndex >= players.Length)
        {
            Debug.LogWarning($"Invalid playerIndex {playerIndex} when assigning gamepad {gamepad.deviceId}.");
            return;
        }

        var player = players[playerIndex];
        if (player == null)
        {
            Debug.LogWarning($"Player input at index {playerIndex} is null. Cannot assign gamepad {gamepad.deviceId}.");
            return;
        }

        player.enabled = true;
        activePlayerAmount++;

        if (multiplayerScreenManager != null)
        {
            try
            {
                if (multiplayerScreenManager.playerData != null)
                    multiplayerScreenManager.playerData[playerIndex].isActive = true;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Could not set playerData[{playerIndex}].isActive: {e.Message}");
            }

            try
            {
                multiplayerScreenManager.UpdatePlayerAmount();
            }
            catch (Exception e)
            {
                Debug.LogWarning($"multiplayerScreenManager.UpdatePlayerAmount() threw: {e.Message}");
            }
        }

        try
        {
            player.SwitchCurrentControlScheme(gamepad);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"SwitchCurrentControlScheme failed for player {playerIndex}: {e.Message}");
        }

        gamepadToPlayer.Add(gamepad, playerIndex);
        takenSlots.Add(playerIndex);

        // Save the assignment and optionally record join order for future scenes
        savedGamepadAssignments[gamepad.deviceId] = playerIndex;
        if (recordOrder)
        {
            if (!savedGamepadOrder.Contains(gamepad.deviceId))
            {
                savedGamepadOrder.Add(gamepad.deviceId);
            }
        }

        if (globalReferanceManager != null && globalReferanceManager.soundManager != null)
            globalReferanceManager.soundManager.PlayPlayerJoinSound();

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
                return;
            }

            // LEAVE (B button)
            if (gamepadToPlayer.ContainsKey(gamepad) &&
                gamepad.yButton.wasPressedThisFrame)
            {
                Leave(gamepad);
                return;
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
        if (!gamepadToPlayer.TryGetValue(gamepad, out int index))
        {
            Debug.LogWarning($"Tried to remove gamepad {gamepad.deviceId} which was not assigned.");
            return;
        }

        var player = (index >= 0 && index < players.Length) ? players[index] : null;
        if (player != null)
            player.enabled = false;

        activePlayerAmount--;
        if (activePlayerAmount < 0) activePlayerAmount = 0; // Safety check

        if (multiplayerScreenManager != null)
        {
            try
            {
                if (multiplayerScreenManager.playerData != null)
                    multiplayerScreenManager.playerData[index].isActive = false;
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Could not set playerData[{index}].isActive: {e.Message}");
            }

            try
            {
                multiplayerScreenManager.UpdatePlayerAmount();
            }
            catch (Exception e)
            {
                Debug.LogWarning($"multiplayerScreenManager.UpdatePlayerAmount() threw: {e.Message}");
            }
        }

        gamepadToPlayer.Remove(gamepad);
        takenSlots.Remove(index);

        // Remove from saved assignments and join order
        savedGamepadAssignments.Remove(gamepad.deviceId);
        savedGamepadOrder.Remove(gamepad.deviceId);

        if (globalReferanceManager != null && globalReferanceManager.soundManager != null)
            globalReferanceManager.soundManager.PlayPlayerDisconnectSound();

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
