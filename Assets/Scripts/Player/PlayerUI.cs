using UnityEngine;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalReferanceManager globalReferenceManager;

    [Header("Currency")]
    [SerializeField] private GameObject currencyDisplayObject;
    [SerializeField] private TextMeshProUGUI currencyDisplay;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] public GameObject canTakeDamageDisplay, canHealDisplay;

    void Start()
    {
        canTakeDamageDisplay.SetActive(false);
        canHealDisplay.SetActive(false);
        if (globalReferenceManager.currency != Currency.SeperateBanks && globalReferenceManager.currency != Currency.SplitEvenly)
            currencyDisplayObject.SetActive(false);
    }

    public void UpdateHealthDisplay(int currentHealthPoints)
    {
        healthDisplay.text = currentHealthPoints.ToString("0");
    }

    internal void UpdateCurrencyDisplay(float currencyAmount)
    {
        currencyDisplay.text = currencyAmount.ToString("0");
    }
}
