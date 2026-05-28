using UnityEngine;
using TMPro;
using System.Collections;
using System;
using Unity.VisualScripting;

public class CampHealth : MonoBehaviour
{
    [Header("Referances")]
    
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay, canHealDisplay;
    

    [Header("Health")]
    [SerializeField] private bool isMainCamp = false;
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealthPoints = 5;
    [SerializeField] private bool HealingIFramesEnabled = false;
    [SerializeField] private bool DamageIFramesEnabled = false;
    
    internal ScreenUI screenUI;
    internal bool destroyed = false;
    public bool canTakeDamage = true;
    public bool canHeal = true;


    void Start()
    {
        if (isMainCamp)
        {
            if (screenUI != null)
                screenUI.UpdateHealthDisplay(maxHealth, currentHealthPoints);
        }
        healthDisplay.text = maxHealth.ToString("0") + " / " + currentHealthPoints.ToString("0") + " hp";
        
        canTakeDamageDisplay.SetActive(false);
        canHealDisplay.SetActive(false);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (destroyed) return;
            if (!canTakeDamage) return;
            EnemyAttack enemyAttack = collision.gameObject.GetComponent<EnemyAttack>();
            if (enemyAttack == null)
            {
                Debug.LogWarning("Enemy is missing EnemyAttack script"); return;
            }

            TakeDamage(enemyAttack.campDamage, 1f);
            Destroy(collision.gameObject);
        }
    }

    
    public void Heal(int healAmount)
    {
        if (canHeal == false)
            return;
        if (currentHealthPoints >= maxHealth)
            return;

        currentHealthPoints = currentHealthPoints + healAmount;
        if (currentHealthPoints > maxHealth)
            currentHealthPoints = maxHealth;
        
        if (isMainCamp)
        {
            if (screenUI != null)
                screenUI.UpdateHealthDisplay(maxHealth, currentHealthPoints);
        }
        healthDisplay.text = maxHealth.ToString("0") + " / " + currentHealthPoints.ToString("0") + " hp";
        
        Debug.Log("Camp healed " + healAmount + " Health!");

        
        if (HealingIFramesEnabled == true)
        {
            canHeal = false;
            canHealDisplay.SetActive(true);
            StartCoroutine(HealingIFrames(1f));
        }
    }

    public void TakeDamage(int damageAmount, float damageFrames)
    {
        if (canTakeDamage == false)
            return;
        if (destroyed)
            return;
        
        currentHealthPoints = currentHealthPoints - damageAmount;
       
        if (isMainCamp)
        {
            if (screenUI != null)
                screenUI.UpdateHealthDisplay(maxHealth, currentHealthPoints);
        }
        healthDisplay.text = maxHealth.ToString("0") + " / " + currentHealthPoints.ToString("0") + " hp";
        
        Debug.Log("Camp took " + damageAmount + " Damage!");

        if (DamageIFramesEnabled == true)
        {
            //canTakeDamage = false;
            canTakeDamageDisplay.SetActive(true);
            StartCoroutine(HurtIFrames(damageFrames));
        }
        
        if (currentHealthPoints <= 0 && destroyed == false)
        {
            destroyed = true;
            DestroyCamp();
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

    private void DestroyCamp()
    {
        if (isMainCamp)
        {
            screenUI.DefeatScreen();
        } 
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("Your camp has been destroyed!");
    }
}
