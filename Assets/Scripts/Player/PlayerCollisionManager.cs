using UnityEngine;
using System.Collections;

public class PlayerCollisionManager : MonoBehaviour
{
    [Header("Referances")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CurrencyManager currencyManager;

    [Header("SpawnPoint")]
    [SerializeField] private Transform spawnpoint;

    [Header("Collision Settings")]
    [SerializeField] private int playerID = 1;

    
    void OnTriggerStay2D(Collider2D collision)
    {
        //Hurt Player when hit Hazard
        if (collision.gameObject.CompareTag("Hazard"))
        {
            if (playerHealth.dead) return;
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
                playerMovement.ApplyKnockback(knockbackDir, hazard.knockbackForce, hazard.knockbackDuration, hazard.stunDuration, hazard.damageTime);
            }
            hazard.HitTarget();
        }
        
        //Heal Player when hit Healing Object
        if (collision.gameObject.CompareTag("Healing"))
        {
            if (playerHealth.dead) return;
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

        //Collect Coin when hit Coin Object
        if (collision.gameObject.CompareTag("Coin"))
        {
            if (playerHealth.dead) return;
            if (!playerHealth.canHeal) return;
            Coin coin = collision.gameObject.GetComponent<Coin>();
            if (coin == null)
            {
                Debug.LogWarning("Coin is missing script"); return;
            }

            currencyManager.AddCurrency(coin.amount, playerID, false);

            Destroy(collision.gameObject);
        }

        //Teleport to Spawn
        if (collision.gameObject.CompareTag("Teleport"))
        {
            gameObject.transform.position = spawnpoint.position;
        }
    }

}
