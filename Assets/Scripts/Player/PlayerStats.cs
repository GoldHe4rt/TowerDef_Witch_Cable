using System;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private int[] playerKills = new int[4]; // Assuming a maximum of 4 players
    [SerializeField] private TextMeshProUGUI killCounterDisplay;
    

    void Start()
    {
        UpdateDisplay();
    }
    public void AddKills(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= playerKills.Length)
            return;

        playerKills[playerIndex]++;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (killCounterDisplay == null)
            return;

        killCounterDisplay.text = $"P1: {playerKills[0]} \nP2: {playerKills[1]} \nP3: {playerKills[2]} \nP4: {playerKills[3]}";
    }
}
