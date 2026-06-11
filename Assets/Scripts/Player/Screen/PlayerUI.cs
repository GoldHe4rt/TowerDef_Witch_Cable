using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GlobalReferanceManager globalReferenceManager;
    [SerializeField] private MultiplayerScreenManager multiplayerScreenManager;
    [SerializeField] private int playerID = -1;

    [Header("Health")]
    [SerializeField] private GameObject[] healthDisplay;
    [SerializeField] public GameObject canTakeDamageDisplay, canHealDisplay;

    [Header("Currency")]
    [SerializeField] private GameObject currencyDisplayObject;
    [SerializeField] private TextMeshProUGUI currencyDisplay;

    [Header("Change Display")]
    [SerializeField] private GameObject changeObject;

    [Header("Building")]
    [SerializeField] private GameObject buildingPriceDisplayObject;
    [SerializeField] private TextMeshProUGUI buildingPriceDisplay, buildingNameDisplay;

    [Header("Death")]
    [SerializeField] internal GameObject gameplayScreen;
    [SerializeField] internal GameObject deathScreen;
    [SerializeField] private TextMeshProUGUI respawnText;

    void Start()
    {
        changeObject.SetActive(false);
        canTakeDamageDisplay.SetActive(false);
        canHealDisplay.SetActive(false);
        deathScreen.SetActive(false);
        if (globalReferenceManager.currency != Currency.SeperateBanks && globalReferenceManager.currency != Currency.SplitEvenly)
            currencyDisplayObject.SetActive(false);
    }

    public void UpdateHealthDisplay(int currentHealthPoints, int changeAmount)
    {
        for (int i = 0; i < healthDisplay.Length; i++)
        {
            if (i < currentHealthPoints)
            {
                healthDisplay[i].SetActive(true);
            } else
            {
                healthDisplay[i].SetActive(false);
            }
        }
        DisplayChangeAmount((float)changeAmount, changeObject, healthDisplay[0].transform, false);
    }

    internal void UpdateCurrencyDisplay(float currencyAmount, float changeAmount)
    {
        currencyDisplay.text = currencyAmount.ToString("0");
        DisplayChangeAmount(changeAmount, changeObject, currencyDisplay.transform, true);
    }

    void DisplayChangeAmount(float changeAmount, GameObject changeObject, Transform parentLocation, bool shouldMove)
    {
        GameObject currentChangeDisplay = Instantiate(changeObject, parentLocation.position, Quaternion.identity, parentLocation);
        currentChangeDisplay.SetActive(true);
        TextMeshProUGUI changeText = currentChangeDisplay.GetComponent<TextMeshProUGUI>();
        
        
        if (changeAmount > 0)
        {
            changeText.text = "+" + changeAmount.ToString("0");
            changeText.color = Color.green;
        }
        else if (changeAmount < 0)
        {
            changeText.text = changeAmount.ToString("0");
            changeText.color = Color.red;
        } 
        else
        {
            changeText.text = "";
        }
        Rigidbody2D currencyChangeRb = currentChangeDisplay.GetComponent<Rigidbody2D>();
        if (currencyChangeRb != null && shouldMove)
        {
            if (multiplayerScreenManager.flipUiY[playerID].isFlipped)
            {
                currencyChangeRb.linearVelocity = Vector2.down * 2f;
            } 
            else if (!multiplayerScreenManager.flipUiY[playerID].isFlipped)
            {
                currencyChangeRb.linearVelocity = Vector2.up * 2f;
            }
        }
        StartCoroutine(TextFade(currentChangeDisplay, changeText, 1f, 3f, false));
    }

    IEnumerator TextFade(GameObject currentChangeDisplay, TextMeshProUGUI myText, float duration, float exponent, bool fadeIn)
    {
        float timeElapsed = 0f;
        Color textColor = myText.color;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            
            // 1. Calculate normalized time (always 0 to 1)
            float t = timeElapsed / duration;

            // 2. Apply the exponential curve to the progress fraction
            float exponentialT = Mathf.Pow(t, exponent);

            // 3. Interpolate between start and end values
            if (fadeIn)
                textColor.a = Mathf.Lerp(0, 1, exponentialT);
            else
                textColor.a = Mathf.Lerp(1, 0, exponentialT);
            myText.color = textColor;

            yield return null; // Wait for the next frame
        }

        // Ensure it strictly ends exactly at the destination value
        if (fadeIn)
        {
            textColor.a = 1;
            myText.color = textColor;
        } else
            Destroy(currentChangeDisplay, 0.1f);
        
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
