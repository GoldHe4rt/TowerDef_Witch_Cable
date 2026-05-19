using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private TextMeshProUGUI healthDisplay;
    [SerializeField] private GameObject canTakeDamageDisplay;
    

    [Header("Health")]
    [SerializeField] private int healthPoints = 5;
    [SerializeField] private int currencyOnDeath = 10;
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

        if (hazard.dealKnockback == true)
        {
            //Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
            //ApplyKnockback(knockbackDir, hazard.knockbackForce, hazard.knockbackDuration, hazard.stunDuration);
        }

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
/*/
    //Knockback
    public void ApplyKnockback(Vector2 direction, float force, float duration, float stun)
    {
        StartCoroutine(KnockbackCoroutine(direction, force, duration, stun));
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction, float force, float duration, float stunTime)
    {
        movementEnabled = false;
        rb.linearVelocity = Vector2.zero; // Reset velocity for consistency
        rb.AddForce(direction * force, ForceMode2D.Impulse); // Apply instant force
        yield return new WaitForSeconds(duration);
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(StunCoroutine(stunTime));
    }

    private IEnumerator StunCoroutine(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        movementEnabled = true;
    }
/*/
}
