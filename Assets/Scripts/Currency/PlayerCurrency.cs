using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerUI playerUI;

    [Header("Currency")]
    [SerializeField] private float currencyAmount = 1234;

    void Start()
    {
        playerUI.UpdateCurrencyDisplay(currencyAmount, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AddCurrency(100);
        }
    }

    internal void SetCurrency(float amount)
    {
        currencyAmount = amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount, 0);
    }

    internal void AddCurrency(float amount)
    {
        currencyAmount += amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount, amount);
    }

    internal void RemoveCurrency(float amount)
    {
        currencyAmount -= amount;
        playerUI.UpdateCurrencyDisplay(currencyAmount, -amount);
    }

    internal float GetCurrencyAmount()
    {
        return currencyAmount;
    }
}
