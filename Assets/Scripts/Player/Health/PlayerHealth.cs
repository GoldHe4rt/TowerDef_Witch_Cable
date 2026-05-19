using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerUI playerUI;
    

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
        playerUI.UpdateHealthDisplay(currentHealthPoints);
    }

    void Update()
    {
        if (currentHealthPoints <= 0 && dead == false)
        {
            dead = true;
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
        playerUI.UpdateHealthDisplay(currentHealthPoints);
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
        currentHealthPoints = currentHealthPoints - damageAmount;
        playerUI.UpdateHealthDisplay(currentHealthPoints);
        //Debug.Log("Took " + damageAmount + " Damage!");

        if (DamageIFramesEnabled == true)
        {
            canTakeDamage = false;
            playerUI.canTakeDamageDisplay.SetActive(true);
            StartCoroutine(HurtIFrames(damageFrames));
        }
            
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
