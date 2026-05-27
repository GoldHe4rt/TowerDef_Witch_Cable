using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay;
    

    [Header("Difficulty Scaling")]
    [SerializeField] private int minHealthPoints = 1;
    [SerializeField] private int maxHealthPoints = 25;
    [SerializeField] private int minCurrencyOnDeath = 10;
    [SerializeField] private int maxCurrencyOnDeath = 100;
    [SerializeField] private float minScale = 0.8f;
    [SerializeField] private float maxScale = 2f;
    private int healthPoints;
    private int currencyOnDeath;
    [SerializeField] private bool iFramesEnabled = true;
    
    internal bool dead = false;
    internal bool invinsible = false;

    void Start()
    {
        healthDisplay.text = healthPoints.ToString("0");
        canTakeDamageDisplay.SetActive(false);
    }
    
    //Hurt Player when hit Hazard
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Damage Dealer")) return;
        if (invinsible) return;
        DamageDealer hazard = collision.gameObject.GetComponent<DamageDealer>();
        if (hazard == null)
        {
            Debug.LogWarning("Damage Dealer is missing DamageDealer script"); return;
        }

        LoseHealth(hazard.damageAmount, hazard.damageTime);

        hazard.HitTarget();

        if (healthPoints <= 0 && dead == false)
        {
            dead = true;
            hazard.KilledTarget(currencyOnDeath);
            Destroy(gameObject);
        }
    }
    
    public void LoseHealth(int damageAmount, float damageFrames)
    {
        healthPoints = healthPoints - damageAmount;
        healthDisplay.text = healthPoints.ToString("0");
        //Debug.Log("Dealt " + damageAmount + " Damage!");

        if (iFramesEnabled == true)
        {
            invinsible = true;
            canTakeDamageDisplay.SetActive(true);
            StartCoroutine(IFrames(damageFrames));
        }
            
    }

    IEnumerator IFrames(float damageFrames)
    {
        yield return new WaitForSeconds(damageFrames);
        canTakeDamageDisplay.SetActive(false);
        invinsible = false;
    }

    internal void SetDifficultyModifier(float difficultyModifier)
    {
        healthPoints = Mathf.RoundToInt(Mathf.Lerp(minHealthPoints, maxHealthPoints, difficultyModifier));
        currencyOnDeath = Mathf.RoundToInt(Mathf.Lerp(minCurrencyOnDeath, maxCurrencyOnDeath, difficultyModifier));
        healthDisplay.text = healthPoints.ToString("0");
        transform.localScale = Vector3.one * Mathf.Lerp(minScale, maxScale, difficultyModifier);
    }
}
