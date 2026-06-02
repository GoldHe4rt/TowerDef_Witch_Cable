using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalReferanceManager globalReferanceManager;
    [SerializeField] private MultiplayerScreenManager multiplayerScreenManager;

    [Header("Camp")]
    [SerializeField] CampCurrency campCurrency;
    [Header("Players")]
    [SerializeField] PlayerCurrency[] playerCurrency;
    

    void Start()
    {
        for (int i = 0; i < multiplayerScreenManager.playerData.Count; i++)
        {
            if (multiplayerScreenManager.playerAmount == 0) return;
            playerCurrency[i].SetCurrency(globalReferanceManager.startCurrency / multiplayerScreenManager.playerAmount);
        }
        campCurrency.SetCurrency(globalReferanceManager.startCurrency);
    }


    internal void SetCurrency(float currency, int playerIndex)
    {
        if (globalReferanceManager.currency == Currency.None)
            return;

        if (globalReferanceManager.currency == Currency.SeperateBanks)
        {
            playerCurrency[playerIndex - 1].SetCurrency(currency);
            return;
        } 

        if (globalReferanceManager.currency == Currency.SplitEvenly)
        {
            for (int i = 0; i < multiplayerScreenManager.playerData.Count; i++)
            {
                if (multiplayerScreenManager.playerData[i].playerObj == null) return;
                
                if (multiplayerScreenManager.playerData[i].isActive)
                {
                    playerCurrency[i].SetCurrency(currency);
                }
            }
            return;
        }

        if (globalReferanceManager.currency == Currency.SharedBank)
        {
            campCurrency.SetCurrency(currency);
            return;
        } 
        
        Debug.LogError("Invalid currency type");
        
    }

    internal void AddCurrency(float currency, int playerIndex, bool killedEnemy)
    {
        if (killedEnemy)
        {
            globalReferanceManager.playerStats.AddKills(playerIndex - 1);
        }
        if (globalReferanceManager.currency == Currency.None)
            return;

        if (globalReferanceManager.currency == Currency.SeperateBanks)
        {
            playerCurrency[playerIndex - 1].AddCurrency(currency);
            return;
        } 

        if (globalReferanceManager.currency == Currency.SplitEvenly)
        {
            for (int i = 0; i < multiplayerScreenManager.playerData.Count; i++)
            {
                if (multiplayerScreenManager.playerData[i].playerObj == null) return;
                
                if (multiplayerScreenManager.playerData[i].isActive)
                {
                    playerCurrency[i].AddCurrency(currency / multiplayerScreenManager.playerAmount);
                }
            }
            return;
        }

        if (globalReferanceManager.currency == Currency.SharedBank)
        {
            campCurrency.AddCurrency(currency);
            return;
        } 
        
        Debug.LogError("Invalid currency type");
        
    }

    internal void RemoveCurrency(float currency, int playerIndex)
    {
        if (globalReferanceManager.currency == Currency.None)
            return;

        if (globalReferanceManager.currency == Currency.SeperateBanks || 
            globalReferanceManager.currency == Currency.SplitEvenly)
        {
            playerCurrency[playerIndex - 1].RemoveCurrency(currency);
            return;
        } 

        if (globalReferanceManager.currency == Currency.SharedBank)
        {
            campCurrency.RemoveCurrency(currency);
            return;
        } 
        
        Debug.LogError("Invalid currency type");
        
    }

    internal bool PlayerHasSufficientCurrency(int playerIndex, float cost)
    {
        if (globalReferanceManager.currency == Currency.None)
            return true;

        if (globalReferanceManager.currency == Currency.SeperateBanks || 
            globalReferanceManager.currency == Currency.SplitEvenly)
        {
            Debug.Log("Player " + playerIndex + " has " + playerCurrency[playerIndex - 1].GetCurrencyAmount() + " currency. Cost is " + cost);
            return playerCurrency[playerIndex - 1].GetCurrencyAmount() >= cost;
        }

        if (globalReferanceManager.currency == Currency.SharedBank)
        {
            Debug.Log("Shared bank has " + campCurrency.GetCurrencyAmount() + " currency. Cost is " + cost);
            return campCurrency.GetCurrencyAmount() >= cost;
        }
        Debug.LogError("Invalid currency type");
        return false;
    }
}

