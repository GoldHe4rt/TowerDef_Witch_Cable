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

    [Header("Death")]
    [SerializeField] internal GameObject gameplayScreen;
    [SerializeField] internal GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI respawnText;

    void Start()
    {
        canTakeDamageDisplay.SetActive(false);
        canHealDisplay.SetActive(false);
        deathScreen.SetActive(false);
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

    internal void UpdateRespawnDisplay(float respawnTime)
    {
        respawnText.text = respawnTime.ToString("0");
    }
}
