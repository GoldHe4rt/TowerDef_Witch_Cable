using System;
using UnityEngine;

using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ControllerManager : MonoBehaviour
{
    
    public PlayerInput[] players; // assign in inspector

    private Dictionary<Gamepad, int> gamepadToPlayer = new Dictionary<Gamepad, int>();
    private HashSet<int> takenSlots = new HashSet<int>();

    private void Update()
    {
        JoinGamepad();
    }

    void JoinGamepad()
    {
        foreach (var gamepad in Gamepad.all)
        {
            // JOIN (A button)
            if (!gamepadToPlayer.ContainsKey(gamepad) &&
                gamepad.aButton.wasPressedThisFrame)
            {
                TryJoin(gamepad);
            }

            // LEAVE (B button)
            if (gamepadToPlayer.ContainsKey(gamepad) &&
                gamepad.selectButton.wasPressedThisFrame)
            {
                StartCoroutine(LeaveTimer(gamepad));
            }
        }
    }

    private IEnumerator LeaveTimer(Gamepad gamepad)
    {       
        int index = gamepadToPlayer[gamepad];
        for (int i = 0; i < 30; i++)
        {
            Debug.Log($"Player {index + 1} is leaving in {(30f - i) / 10f} seconds");
            if (!gamepad.selectButton.isPressed)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        }
        Leave(gamepad);
    }

    void TryJoin(Gamepad gamepad)
    {
        int freeIndex = GetFreePlayerIndex();
        if (freeIndex == -1) return;

        var player = players[freeIndex];

        player.enabled = true;
        player.SwitchCurrentControlScheme(gamepad);

        gamepadToPlayer.Add(gamepad, freeIndex);
        takenSlots.Add(freeIndex);

        Debug.Log($"Player {freeIndex + 1} joined");
    }

    void Leave(Gamepad gamepad)
    {
        int index = gamepadToPlayer[gamepad];
        var player = players[index];

        // Disable input (or whole player if you want)
        player.enabled = false;

        gamepadToPlayer.Remove(gamepad);
        takenSlots.Remove(index);

        Debug.Log($"Player {index + 1} left");
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
