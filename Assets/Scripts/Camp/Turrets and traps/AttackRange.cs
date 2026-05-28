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
        if (!collision.gameObject.CompareTag("Enemy"))
            return;
        if (towerAttack.towerType == TowerType.Turret)
        {
            if (currentAimTarget != null)
                return;
            currentAimTarget = collision.gameObject;
        }
        if (towerAttack.towerType == TowerType.Explosive)
        {
            towerAttack.Explode();
        }
        if (towerAttack.towerType == TowerType.Spike)
        {
            towerAttack.Attack();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentAimTarget)
            currentAimTarget = null;
    }
}
