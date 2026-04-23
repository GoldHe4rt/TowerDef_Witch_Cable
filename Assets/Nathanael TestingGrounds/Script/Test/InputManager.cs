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

    private List<Gamepad> joinedGamepads = new List<Gamepad>();
    private int nextPlayerIndex = 0;

    void JoinGamepad()
    {
        foreach (var gamepad in Gamepad.all)
        {
            // Skip already joined controllers
            if (joinedGamepads.Contains(gamepad))
                continue;

            // Check if A button was pressed
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                JoinPlayer(gamepad);
            }
        }
    }

    void JoinPlayer(Gamepad gamepad)
    {
        if (nextPlayerIndex >= players.Length)
            return;

        var player = players[nextPlayerIndex];

        player.enabled = true;
        player.SwitchCurrentControlScheme(gamepad);

        joinedGamepads.Add(gamepad);
        nextPlayerIndex++;

        Debug.Log("Player joined with: " + gamepad.displayName);
    }
}
