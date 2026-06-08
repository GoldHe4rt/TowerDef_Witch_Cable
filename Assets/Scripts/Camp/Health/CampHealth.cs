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
    
    [SerializeField] private int maxHealth = 5;
    [SerializeField] internal int currentHealthPoints = 5;
    [SerializeField] private bool HealingIFramesEnabled = false;
    [SerializeField] private bool DamageIFramesEnabled = false;

    [Header("Defeat")]
    [SerializeField] private bool isMainCamp = false;
    [SerializeField] private GameObject defeatCamera;
    [SerializeField] private Animator defeatAnimator;
    
    internal ScreenUI screenUI;
    internal bool destroyed = false;
    public bool canTakeDamage = true;
    public bool canHeal = true;
    private float healTimer = 60f;


    void Start()
    {
        if (isMainCamp)
        {
            if (screenUI != null)
                screenUI.UpdateHealthDisplay(maxHealth, currentHealthPoints, 0);
            if (defeatCamera != null)
                defeatCamera.SetActive(false);
        }
        healthDisplay.text = currentHealthPoints.ToString("0") + " / " + maxHealth.ToString("0") + " hp";
        
        canTakeDamageDisplay.SetActive(false);
        canHealDisplay.SetActive(false);
    }
    void Update()
    {
        healTimer -= Time.deltaTime;
        if (healTimer <= 0)
        {
            healTimer = 60f; // Reset the timer
            Heal(1); // Heal 1 point per minute
        }
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
                screenUI.UpdateHealthDisplay(maxHealth, currentHealthPoints, healAmount);
        }
        healthDisplay.text = currentHealthPoints.ToString("0") + " / " + maxHealth.ToString("0") + " hp";
        
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
                screenUI.UpdateHealthDisplay(maxHealth, currentHealthPoints, -damageAmount);
        }
        healthDisplay.text = currentHealthPoints.ToString("0") + " / " + maxHealth.ToString("0") + " hp";
        
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
            StartCoroutine(Defeat());
            
        } 
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("Your camp has been destroyed!");
    }

    private void OnValidate()
    {
        if (isMainCamp)
        {
            if (screenUI != null)
                screenUI.UpdateHealthDisplay(maxHealth, currentHealthPoints, 0);
        }
        healthDisplay.text = currentHealthPoints.ToString("0") + " / " + maxHealth.ToString("0") + " hp";
    }

    IEnumerator Defeat()
    {
        if (defeatCamera != null)
            defeatCamera.SetActive(true);
        Time.timeScale = 0.1f;
        defeatAnimator.SetBool("Defeat", true);
        yield return new WaitForSeconds(0.2f);
        screenUI.globalReferanceManager.soundManager.PlayBaseDamageSound();
        yield return new WaitForSeconds(0.1f);
        screenUI.DefeatScreen();
    }
}
