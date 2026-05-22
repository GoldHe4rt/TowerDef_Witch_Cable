using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyRange : MonoBehaviour
{
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] float attackRange = 10f;
    internal GameObject currentAimTarget;

    void Awake()
    {
        currentAimTarget = null;
        gameObject.transform.localScale = new Vector3(attackRange, attackRange, 1);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        if (currentAimTarget != null)
            return;
        
        currentAimTarget = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentAimTarget)
            currentAimTarget = null;
    }
}
