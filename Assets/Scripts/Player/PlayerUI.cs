using UnityEngine;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalReferanceManager globalReferenceManager;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] public GameObject canTakeDamageDisplay, canHealDisplay;

    [Header("Currency")]
    [SerializeField] private GameObject currencyDisplayObject;
    [SerializeField] private TextMeshProUGUI currencyDisplay;
    [SerializeField] private GameObject currencyChangeObject;

    [Header("Building")]
    [SerializeField] private GameObject buildingPriceDisplayObject;
    [SerializeField] private TextMeshProUGUI buildingPriceDisplay, buildingNameDisplay;

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

    internal void UpdateCurrencyDisplay(float currencyAmount, float changeAmount)
    {
        currencyDisplay.text = currencyAmount.ToString("0");
        GameObject currentChangeDisplay = Instantiate(currencyChangeObject, currencyDisplay.transform.position, Quaternion.identity, currencyDisplay.transform);
        currentChangeDisplay.SetActive(true);
        TextMeshProUGUI changeText = currentChangeDisplay.GetComponent<TextMeshProUGUI>();
        
        
        if (changeAmount > 0)
        {
            changeText.text = "+" + changeAmount.ToString("0");
            changeText.color = Color.green;
        }
        else if (changeAmount < 0)
        {
            changeText.text = "-" + changeAmount.ToString("0");
            changeText.color = Color.red;
        } 
        else
        {
            changeText.text = "";
        }
        Rigidbody2D currencyChangeRb = currentChangeDisplay.GetComponent<Rigidbody2D>();
        if (currencyChangeRb != null)
        {
            currencyChangeRb.linearVelocity = Vector2.down * 2f;
        }
        Destroy(currentChangeDisplay, 1f);
    }

    internal void UpdateRespawnDisplay(float respawnTime)
    {
        respawnText.text = respawnTime.ToString("0");
    }

    internal void UpdateBuildingCostDisplay(float cost)
    {
        if (cost == -1)
        {
            buildingPriceDisplayObject.SetActive(false);
            return;
        }
        buildingPriceDisplayObject.SetActive(true);
        buildingPriceDisplay.text = cost.ToString("0") + "$";
    }

    internal void UpdateBuildingNameDisplay(string structureName)
    {
        buildingNameDisplay.text = structureName;
    }
}
