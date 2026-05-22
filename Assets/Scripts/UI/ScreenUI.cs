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

    [Header("Defeat Screen")]
    [SerializeField] private GameObject defeatScreen;

    void Awake()
    {
        globalReferenceManager.campHealth.screenUI = this;
    }

    void Start()
    {
        damageDisplay.SetActive(false);
        if (globalReferenceManager.currency != Currency.SharedBank)
            currencyDisplayObject.SetActive(false);
        
    }

    public void UpdateHealthDisplay(int maxHealth, int healthPoints)
    {
        healthDisplay.text = maxHealth.ToString("0") + " / " + healthPoints.ToString("0");
    }

    internal void UpdateCurrencyDisplay(float currencyAmount)
    {
        currencyDisplay.text = currencyAmount.ToString("0");
    }

    internal void DefeatScreen()
    {
        defeatScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
