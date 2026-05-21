using UnityEngine;

public class CampCurrency : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScreenUI screenUI;

    [Header("Currency")]
    [SerializeField] private float currencyAmount = 1234;

    void Start()
    {
        screenUI.UpdateCurrencyDisplay(currencyAmount);
    }
    
    internal void SetCurrency(float amount)
    {
        currencyAmount = amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void AddCurrency(float amount)
    {
        currencyAmount += amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void RemoveCurrency(float amount)
    {
        currencyAmount -= amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal float GetCurrencyAmount()
    {
        return currencyAmount;
    }
}
