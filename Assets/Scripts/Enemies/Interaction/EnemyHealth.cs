using UnityEngine;
using TMPro;
using System.Collections;
using System;
using AudioScripts;
using Random = UnityEngine.Random;

public class EnemyHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay;
    [SerializeField] private LootSystem lootSystem;
    [SerializeField] private GameObject enemyDeathAniObj;
    
    [Header("Default Values")]
    [SerializeField] internal int healthPoints = 10;
    [SerializeField] internal int currencyOnDeath = 50;
    [SerializeField] internal bool iFramesEnabled = false;

    [Header("Difficulty Scaling")]
    [SerializeField] private Vector2 minMaxHealthPoints = new Vector2(1, 25);
    [SerializeField] private Vector2 minMaxCurrencyOnDeath = new Vector2(10, 100);
    [SerializeField] private Vector2 minMaxScale = new Vector2(0.8f, 2f);

    [Header("Drop On Defeat")]
    [Range(0,1)][SerializeField] private float dropChanse;
    
    internal bool dead = false;
    internal bool invinsible = false;
    internal AudioEventManager audioEventManager;

    void Start()
    {
        healthDisplay.text = healthPoints.ToString("0");
        canTakeDamageDisplay.SetActive(false);
    }
    
    public void LoseHealth(int damageAmount, float damageFrames)
    {
        healthPoints = healthPoints - damageAmount;
        healthDisplay.text = healthPoints.ToString("0");
        if (audioEventManager != null)
            audioEventManager.PlayEnemyDamageSound();
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
        yield return new WaitForSeconds(damageFrames * 0.5f);
        canTakeDamageDisplay.SetActive(false);
        //invinsible = false;
    }

    internal void Death()
    {
        lootSystem.RollLoot();
        if (enemyDeathAniObj != null)
            Instantiate(enemyDeathAniObj, transform.position, transform.rotation);
        if (audioEventManager != null)
            audioEventManager.PlayEnemyDefeatSound();
        Destroy(gameObject);
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

