using UnityEngine;

public class CampCurrency : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScreenUI screenUI;

    [Header("Currency")]
    [SerializeField] private int currencyAmount = 100;

    void Start()
    {
        screenUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void AddCurrency(int amount)
    {
        currencyAmount += amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal void RemoveCurrency(int amount)
    {
        currencyAmount -= amount;
        screenUI.UpdateCurrencyDisplay(currencyAmount);
    }

    internal int GetCurrencyAmount()
    {
        return currencyAmount;
    }
}
