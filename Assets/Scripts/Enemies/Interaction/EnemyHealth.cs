using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay;
    
    [Header("Default Values")]
    [SerializeField] internal int healthPoints = 10;
    [SerializeField] internal int currencyOnDeath = 50;
    [SerializeField] internal bool iFramesEnabled = false;

    [Header("Difficulty Scaling")]
    [SerializeField] private Vector2 minMaxHealthPoints = new Vector2(1, 25);
    [SerializeField] private Vector2 minMaxCurrencyOnDeath = new Vector2(10, 100);
    [SerializeField] private Vector2 minMaxScale = new Vector2(0.8f, 2f);
    
    internal bool dead = false;
    internal bool invinsible = false;

    void Start()
    {
        healthDisplay.text = healthPoints.ToString("0");
        canTakeDamageDisplay.SetActive(false);
    }
    
    public void LoseHealth(int damageAmount, float damageFrames)
    {
        healthPoints = healthPoints - damageAmount;
        healthDisplay.text = healthPoints.ToString("0");
        //Debug.Log("Dealt " + damageAmount + " Damage!");

        if (iFramesEnabled == true)
        {
            //invinsible = true;
            canTakeDamageDisplay.SetActive(true);
            StartCoroutine(IFrames(damageFrames));
        }
            
    }

    IEnumerator IFrames(float damageFrames)
    {
        yield return new WaitForSeconds(damageFrames);
        canTakeDamageDisplay.SetActive(false);
        //invinsible = false;
    }

    internal void SetDifficultyModifier(float difficultyModifier)
    {
        if (difficultyModifier <= 1)
        {
            healthPoints = Mathf.RoundToInt(Mathf.Lerp(minMaxHealthPoints.x, minMaxHealthPoints.y, difficultyModifier));
            currencyOnDeath = Mathf.RoundToInt(Mathf.Lerp(minMaxCurrencyOnDeath.x, minMaxCurrencyOnDeath.y, difficultyModifier));
            healthDisplay.text = healthPoints.ToString("0");
            transform.localScale = Vector3.one * Mathf.Lerp(minMaxScale.x, minMaxScale.y, difficultyModifier);
        }
        else
        {
            healthPoints = Mathf.RoundToInt(minMaxHealthPoints.y * difficultyModifier);
            currencyOnDeath = Mathf.RoundToInt(minMaxCurrencyOnDeath.y * difficultyModifier);
            healthDisplay.text = healthPoints.ToString("0");
            transform.localScale = Vector3.one * minMaxScale.y;
        }
    }
}
