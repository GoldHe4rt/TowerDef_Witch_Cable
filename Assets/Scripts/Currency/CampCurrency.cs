using UnityEngine;

public class CampCurrency : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScreenUI screenUI;

    [Header("Currency")]
    [SerializeField] private float currencyAmount = 1234;

    void Start()
    {
        screenUI.UpdateCurrencyDisplay(currencyAmount, 0);
    }
    
    internal void SetCurrency(float amount)
    {
        currencyAmount = amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount, 0);
    }

    internal void AddCurrency(float amount)
    {
        currencyAmount += amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount, amount);
    }

    internal void RemoveCurrency(float amount)
    {
        currencyAmount -= amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount, -amount);
    }

    internal float GetCurrencyAmount()
    {
        return currencyAmount;
    }
    
}
