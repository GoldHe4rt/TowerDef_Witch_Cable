using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{

    public event Action OnClicked, OnExit;

    private void Update()
    {
        JoinGamepad();
        
        if (Input.GetMouseButtonDown(0))
            OnClicked?.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            OnExit?.Invoke();
    }

    public bool IsPointerOverUI()
        => EventSystem.current.IsPointerOverGameObject();




    public PlayerInput[] players; // assign in inspector

    private Dictionary<Gamepad, int> gamepadToPlayer = new Dictionary<Gamepad, int>();
    private HashSet<int> takenSlots = new HashSet<int>();

    void JoinGamepad()
    {
        foreach (var gamepad in Gamepad.all)
        {
            // JOIN (A button)
            if (!gamepadToPlayer.ContainsKey(gamepad) &&
                gamepad.buttonSouth.wasPressedThisFrame)
            {
                TryJoin(gamepad);
            }

            // LEAVE (B button)
            if (gamepadToPlayer.ContainsKey(gamepad) &&
                gamepad.buttonEast.wasPressedThisFrame)
            {
                Leave(gamepad);
            }
        }
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
