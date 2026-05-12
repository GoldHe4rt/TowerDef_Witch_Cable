using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay, canHealDisplay;
    

    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealthPoints = 5;
    [SerializeField] private bool HealingIFramesEnabled = true;
    [SerializeField] private bool DamageIFramesEnabled = true;
    
    [HideInInspector] public bool death = false;
    public bool canTakeDamage = true;
    public bool canHeal = true;


    void Start()
    {
        healthDisplay.text = currentHealthPoints.ToString("0");
        canTakeDamageDisplay.SetActive(false);
        canHealDisplay.SetActive(false);
    }

    void Update()
    {
        if (currentHealthPoints <= 0 && death == false)
        {
            death = true;
            Debug.Log("You are out of Health");
        }
    }
    
    public void Heal(int healAmount)
    {
        if (currentHealthPoints >= maxHealth)
            return;

        currentHealthPoints = currentHealthPoints + healAmount;
        if (currentHealthPoints > maxHealth)
            currentHealthPoints = maxHealth;
        healthDisplay.text = currentHealthPoints.ToString("0");
        Debug.Log("Healed " + healAmount + " Health!");

        
        if (HealingIFramesEnabled == true)
        {
            canHeal = false;
            canHealDisplay.SetActive(true);
            StartCoroutine(HealingIFrames(1f));
        }
    }

    public void LoseHealth(int damageAmount, float damageFrames)
    {
        currentHealthPoints = currentHealthPoints - damageAmount;
        healthDisplay.text = currentHealthPoints.ToString("0");
        Debug.Log("Took " + damageAmount + " Damage!");

        if (DamageIFramesEnabled == true)
        {
            canTakeDamage = false;
            canTakeDamageDisplay.SetActive(true);
            StartCoroutine(HurtIFrames(damageFrames));
        }
            
    }

    private IEnumerator HealingIFrames(float healFrames)
    {
        yield return new WaitForSeconds(healFrames);
        canHealDisplay.SetActive(false);
        canHeal = true;
    }

    private IEnumerator HurtIFrames(float damageFrames)
    {
        yield return new WaitForSeconds(damageFrames);
        canTakeDamageDisplay.SetActive(false);
        canTakeDamage = true;
    }
}
