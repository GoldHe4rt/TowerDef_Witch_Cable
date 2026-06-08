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

    internal void Death()
    {
        //if (Random.value > dropChanse)
        Destroy(gameObject);
    }
/*/
    [System.Serializable]
    public struct LootItem
    {
        public string itemName;
        [Range(0, 100)] public float dropChance; 
    }

    public LootItem[] possibleLoot;

    public void RollLoot()
    {
        // 1. Calculate the total weight of all items
        float totalChance = 0f;
        foreach (LootItem item in possibleLoot)
        {
            totalChance += item.dropChance;
        }

        // 2. Pick a random number within the total
        float randomRoll = Random.Range(0f, totalChance);

        // 3. Find which item the roll landed on
        float cumulativeChance = 0f;
        foreach (LootItem item in possibleLoot)
        {
            cumulativeChance += item.dropChance;
            if (randomRoll <= cumulativeChance)
            {
                Debug.Log($"You rolled: {item.itemName}");
                return;
            }
        }
    }/*/
}

