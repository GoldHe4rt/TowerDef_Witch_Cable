using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerUI playerUI;

    [Header("Currency")]
    [SerializeField] private float currencyAmount = 1234;

    void Start()
    {
        playerUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void SetCurrency(float amount)
    {
        currencyAmount = amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void AddCurrency(float amount)
    {
        currencyAmount += amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void RemoveCurrency(float amount)
    {
        currencyAmount -= amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal float GetCurrencyAmount()
    {
        return currencyAmount;
    }
}
