using System;
using UnityEngine;

using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private MultiplayerScreenManager multiplayerScreenManager;
    public PlayerInput[] players; // assign in inspector
    public int activePlayerAmount = 0;

    private Dictionary<Gamepad, int> gamepadToPlayer = new Dictionary<Gamepad, int>();
    private HashSet<int> takenSlots = new HashSet<int>();
    /*/
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            foreach (var player in players)
            {
                if (player != null)
                    DontDestroyOnLoad(player.gameObject);
            }
        }
    /*/
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
                //Leave(gamepad);
            }
        }
    }


    void TryJoin(Gamepad gamepad)
    {
        int freeIndex = GetFreePlayerIndex();
        if (freeIndex == -1) return;

        var player = players[freeIndex];

        player.enabled = true;
        activePlayerAmount++;
        multiplayerScreenManager.playerData[freeIndex].isActive = true;
        if (multiplayerScreenManager != null)
            multiplayerScreenManager.UpdatePlayerAmount();

        player.SwitchCurrentControlScheme(gamepad);

        gamepadToPlayer.Add(gamepad, freeIndex);
        takenSlots.Add(freeIndex);

        Debug.Log($"Player {freeIndex + 1} joined. Active players: {activePlayerAmount}");
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
