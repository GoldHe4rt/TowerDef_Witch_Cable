using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerUI playerUI;

    [Header("Currency")]
    [SerializeField] private int currencyAmount = 100;

    void Start()
    {
        playerUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void AddCurrency(int amount)
    {
        currencyAmount += amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void RemoveCurrency(int amount)
    {
        currencyAmount -= amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal int GetCurrencyAmount()
    {
        return currencyAmount;
    }
}
