using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System;
using System.Collections;

public class EnemyCollisionManager : MonoBehaviour
{
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private NavMeshPathfinding navMeshPathfinding;
    private List<Collider2D> damageCooldown = new List<Collider2D>();


    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Damage Dealer")) return;
        if (enemyHealth.invinsible) return;
        foreach (Collider2D savedCollider in damageCooldown)
        {
            if (savedCollider == collision) return;
        }

        DamageDealer hazard = collision.gameObject.GetComponent<DamageDealer>();
        if (hazard == null)
        {
            hazard = collision.gameObject.GetComponentInParent<DamageDealer>();
            if (hazard == null)
            {
                Debug.LogWarning("Damage Dealer is missing DamageDealer script"); 
                return;
            }
                
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

        
        StartCoroutine(DamageDealerHurtCooldown(collision, hazard));
        hazard.HitTarget();
    }

    private IEnumerator DamageDealerHurtCooldown(Collider2D savedCollider,DamageDealer hazard)
    {
        damageCooldown.Add(savedCollider);
        yield return new WaitForSeconds(hazard.hurtTime);
        damageCooldown.Remove(savedCollider);
    }
}
