using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;

    //Hurt Player when hit Hazard
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            if (playerHealth.death) return;
            if (!playerHealth.canTakeDamage) return;
            Hazard hazard = collision.gameObject.GetComponent<Hazard>();
            if (hazard == null)
            {
                Debug.LogWarning("Hazard is missing script"); return;
            }

            playerHealth.LoseHealth(hazard.damageAmount, hazard.damageTime);

            if (hazard.dealKnockback == true)
            {
                Vector2 knockbackDir = (transform.position - collision.transform.position).normalized;
                playerMovement.ApplyKnockback(knockbackDir, hazard.knockbackForce, hazard.knockbackDuration, hazard.stunDuration);
            }
            if (hazard.destroyOnTrigger == true)
                Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.CompareTag("Healing"))
        {
            if (playerHealth.death) return;
            if (!playerHealth.canHeal) return;
            HealingObject healingObject = collision.gameObject.GetComponent<HealingObject>();
            if (healingObject == null)
            {
                Debug.LogWarning("Healing Object is missing script"); return;
            }

            playerHealth.Heal(healingObject.healAmount);

            if (healingObject.destroyOnTrigger == true)
                Destroy(collision.gameObject);
        }
    }

}
