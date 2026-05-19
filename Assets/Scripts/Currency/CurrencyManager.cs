using System;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalReferanceManager globalReferanceManager;

    [Header("Camp Currency")]
    [SerializeField] CampCurrency campCurrency;
    [Header("Player Currencies")]
    [SerializeField] PlayerCurrency player1Currency, player2Currency, player3Currency, player4Currency;

    internal void AddCurrency(int currency, int playerIndex)
    {
        if (!globalReferanceManager.currencyEnabled)
            return;

        if (globalReferanceManager.splitCurrency)
        {
            switch (playerIndex)
            {
                case 1:
                    player1Currency.AddCurrency(currency);
                    break;
                case 2:
                    player2Currency.AddCurrency(currency);
                    break;
                case 3:
                    player3Currency.AddCurrency(currency);
                    break;
                case 4:
                    player4Currency.AddCurrency(currency);
                    break;
            }
        }
        else
        {
            campCurrency.AddCurrency(currency);
        }
    }

    internal void RemoveCurrency(int currency, int playerIndex)
    {
        if (!globalReferanceManager.currencyEnabled)
            return;

        if (globalReferanceManager.splitCurrency)
        {
            switch (playerIndex)
            {
                case 1:
                    player1Currency.RemoveCurrency(currency);
                    break;
                case 2:
                    player2Currency.RemoveCurrency(currency);
                    break;
                case 3:
                    player3Currency.RemoveCurrency(currency);
                    break;
                case 4:
                    player4Currency.RemoveCurrency(currency);
                    break;
            }
        }
        else
        {
            campCurrency.RemoveCurrency(currency);
        }
    }

    internal bool PlayerHasSufficientCurrency(int playerIndex, int cost)
    {
        if (!globalReferanceManager.currencyEnabled)
            return true;

        if (globalReferanceManager.splitCurrency)
        {
            switch (playerIndex)
            {
                case 1:
                    return player1Currency.GetCurrencyAmount() >= cost;
                case 2:
                    return player2Currency.GetCurrencyAmount() >= cost;
                case 3:
                    return player3Currency.GetCurrencyAmount() >= cost;
                case 4:
                    return player4Currency.GetCurrencyAmount() >= cost;
            }
        }
        else
        {
            return campCurrency.GetCurrencyAmount() >= cost;
        }
        return false;
    }
}
