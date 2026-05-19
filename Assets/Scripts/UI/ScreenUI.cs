using UnityEngine;
using TMPro;
using System;

public class ScreenUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalReferanceManager globalReferenceManager;

    [Header("Currency")]
    [SerializeField] private GameObject currencyDisplayObject;
    [SerializeField] private TextMeshProUGUI currencyDisplay;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] public GameObject damageDisplay;

    void Start()
    {
        damageDisplay.SetActive(false);
        if (globalReferenceManager.splitCurrency)
            currencyDisplayObject.SetActive(false);
    }

    public void UpdateHealthDisplay(int healthPoints)
    {
        healthDisplay.text = healthPoints.ToString("0");
    }

    internal void UpdateCurrencyDisplay(int currencyAmount)
    {
        currencyDisplay.text = currencyAmount.ToString("0");
    }
}
