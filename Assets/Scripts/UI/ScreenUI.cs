using UnityEngine;
using TMPro;
using System;
using Menu;
using System.Collections;

public class ScreenUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] internal GlobalReferanceManager globalReferanceManager;

    [Header("Currency")]
    [SerializeField] private GameObject currencyDisplayObject;
    [SerializeField] private TextMeshProUGUI currencyDisplay;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] public GameObject damageDisplay;
    
    [Header("Change Display")]
    [SerializeField] private GameObject changeObject;


    void Awake()
    {
        if (globalReferanceManager.campHealth != null)
             globalReferanceManager.campHealth.screenUI = this;
    }

    void Start()
    {
        changeObject.SetActive(false);
        damageDisplay.SetActive(false);
        if (globalReferanceManager.currency != Currency.SharedBank)
            currencyDisplayObject.SetActive(false);
        
    }

    public void UpdateHealthDisplay(int maxHealth, int healthPoints, int changeAmount)
    {
        healthDisplay.text = healthPoints.ToString("0");
        DisplayChangeAmount((float)changeAmount, changeObject, healthDisplay.transform);
    }

    internal void UpdateCurrencyDisplay(float currencyAmount, float changeAmount)
    {
        currencyDisplay.text = currencyAmount.ToString("0");
        DisplayChangeAmount(changeAmount, changeObject, currencyDisplay.transform);
        
    }

    void DisplayChangeAmount(float changeAmount, GameObject changeObject, Transform parentLocation)
    {
        GameObject currentChangeDisplay = Instantiate(changeObject, parentLocation.position, Quaternion.identity, parentLocation);
        currentChangeDisplay.SetActive(true);
        TextMeshProUGUI changeText = currentChangeDisplay.GetComponent<TextMeshProUGUI>();
        
        
        if (changeAmount > 0)
        {
            changeText.text = "+" + changeAmount.ToString("0");
            changeText.color = Color.green;
            if (globalReferanceManager.soundManager != null)
                globalReferanceManager.soundManager.PlayPlayerHealSound();
        }
        else if (changeAmount < 0)
        {
            changeText.text = changeAmount.ToString("0");
            changeText.color = Color.red;
            if (globalReferanceManager.soundManager != null)
                globalReferanceManager.soundManager.PlayBaseDamageSound();
            
        } 
        else
        {
            changeText.text = "";
        }
        Rigidbody2D currencyChangeRb = currentChangeDisplay.GetComponent<Rigidbody2D>();
        if (currencyChangeRb != null)
        {
            currencyChangeRb.linearVelocity = Vector2.down * 200f;
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

    internal void DefeatScreen()
    {
        globalReferanceManager.gameOverController.ShowGameOver();
        Time.timeScale = 0f;
    }
}
