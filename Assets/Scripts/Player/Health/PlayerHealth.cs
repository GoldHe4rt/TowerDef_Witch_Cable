using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private GlobalReferanceManager globalReferanceManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerUI playerUI;

    [Header("Death")]
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private Transform deathSpawnPoint;
    [SerializeField] private float respawntimer = 5f;


    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealthPoints = 5;
    [SerializeField] private bool HealingIFramesEnabled = true;
    [SerializeField] private bool DamageIFramesEnabled = true;
    
    internal bool dead = false;
    public bool canTakeDamage = true;
    public bool canHeal = true;


    void Start()
    {
        playerUI.UpdateHealthDisplay(currentHealthPoints, 0);
    }
    
    public void Heal(int healAmount)
    {
        if (currentHealthPoints >= maxHealth)
            return;

        currentHealthPoints = currentHealthPoints + healAmount;
        if (currentHealthPoints > maxHealth)
            currentHealthPoints = maxHealth;
        playerUI.UpdateHealthDisplay(currentHealthPoints, healAmount);
        globalReferanceManager.soundManager.PlayPlayerHealSound();
        //Debug.Log("Healed " + healAmount + " Health!");

        
        if (HealingIFramesEnabled == true)
        {
            canHeal = false;
            playerUI.canHealDisplay.SetActive(true);
            StartCoroutine(HealingIFrames(1f));
        }
    }

    public void LoseHealth(int damageAmount, float damageFrames)
    {
        if (dead) return;
        currentHealthPoints = currentHealthPoints - damageAmount;
        playerUI.UpdateHealthDisplay(currentHealthPoints, -damageAmount);
        globalReferanceManager.soundManager.PlayPlayerDamageSound();
        //Debug.Log("Took " + damageAmount + " Damage!");

        if (currentHealthPoints <= 0 && dead == false)
        {
            Debug.Log("Player has died!");
            Death();
            return;
        }

        if (DamageIFramesEnabled == true)
        {
            canTakeDamage = false;
            playerUI.canTakeDamageDisplay.SetActive(true);
            StartCoroutine(HurtIFrames(damageFrames));
        }
            
    }

    private void Death()
    {
        dead = true;
        playerMovement.movementEnabled = false;
        playerUI.UpdateHealthDisplay(currentHealthPoints, 0);
        playerUI.gameplayScreen.SetActive(false);
        playerUI.deathScreen.SetActive(true);
        globalReferanceManager.soundManager.PlayPlayerDeathSound();
        StartCoroutine(RespawnCountdown(respawntimer));
    }

    private void Respawn(int healthToHeal)
    {
        Heal(healthToHeal);
        dead = false;

        playerCharacter.transform.position = deathSpawnPoint.position;
        playerMovement.movementEnabled = true;

        playerUI.gameplayScreen.SetActive(true);
        playerUI.deathScreen.SetActive(false);

    }

    private IEnumerator RespawnCountdown(float respawntimer)
    {
        int healthToHeal = 0;
        if (globalReferanceManager.campHealth.currentHealthPoints > maxHealth * 1.5f)
        {
            globalReferanceManager.campHealth.TakeDamage(maxHealth, 0f);
            healthToHeal = maxHealth;
        } 
        else if (globalReferanceManager.campHealth.currentHealthPoints > 1)
        {
            healthToHeal = globalReferanceManager.campHealth.currentHealthPoints;
            globalReferanceManager.campHealth.TakeDamage(1, 0f);
        } 
        else if (globalReferanceManager.campHealth.currentHealthPoints == 1)
        {
            healthToHeal = 1;
        }
        while (respawntimer > 0)
        {
            playerUI.UpdateRespawnDisplay(respawntimer);
            yield return new WaitForSeconds(1f);
            respawntimer -= 1f;
        }
        playerUI.UpdateHealthDisplay(currentHealthPoints, 0);
        playerUI.UpdateRespawnDisplay(respawntimer);
        yield return new WaitForSeconds(0.2f);
        Respawn(healthToHeal);
    }

    private IEnumerator HealingIFrames(float healFrames)
    {
        yield return new WaitForSeconds(healFrames);
        playerUI.canHealDisplay.SetActive(false);
        canHeal = true;
    }

    private IEnumerator HurtIFrames(float damageFrames)
    {
        yield return new WaitForSeconds(damageFrames);
        playerUI.canTakeDamageDisplay.SetActive(false);
        canTakeDamage = true;
    }
}
