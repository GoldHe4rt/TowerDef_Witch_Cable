using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackRange : MonoBehaviour
{
    [SerializeField] private TowerAttack towerAttack;
    [SerializeField] float attackRange = 10f;
    internal GameObject currentAimTarget;

    void Awake()
    {
        currentAimTarget = null;
        gameObject.transform.localScale = new Vector3(attackRange, attackRange, 1);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (towerAttack.towerType)
            {
                case TowerType.Turret:
                    if (currentAimTarget != null)
                        return;
                    currentAimTarget = collision.gameObject;
                    break;
                
                case TowerType.Explosive:
                    towerAttack.Explode();
                    break;
                
                case TowerType.Spike:
                    towerAttack.Attack();
                    break;
                
                case TowerType.Freeze:
                    towerAttack.Freeze();
                    break;
            }
        }
        if (collision.gameObject.CompareTag("Hazard") && towerAttack.towerType == TowerType.Barricade)
        {
            Hazard hazard = collision.gameObject.GetComponent<Hazard>();
            hazard.HitTarget();

            towerAttack.TakeDamage(hazard.damageAmount); // Replace 1 with the actual damage amount
        }
        

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentAimTarget)
            currentAimTarget = null;
    }
}
