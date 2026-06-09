using UnityEngine;
using UnityEngine.AI;

public class EnemyCollisionManager : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private NavMeshPathfinding navMeshPathfinding;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Damage Dealer")) return;
        if (enemyHealth.invinsible) return;
        DamageDealer hazard = collision.gameObject.GetComponent<DamageDealer>();
        if (hazard == null)
        {
            Debug.LogWarning("Damage Dealer is missing DamageDealer script"); return;
        }

        enemyHealth.LoseHealth(hazard.damageAmount, hazard.hurtTime);
        if (hazard.freezeTarget)
        {
            Debug.Log("Enemy frozen!");
            StartCoroutine(navMeshPathfinding.SpeedRecovery(hazard.freezeDuration, 2f, 0.3f, 1f));
        }
        if (enemyHealth.healthPoints <= 0)
        {
            hazard.KilledTarget(enemyHealth.currencyOnDeath);
            enemyHealth.Death();
        }
        
        hazard.HitTarget();
    }
}
