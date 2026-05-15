using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackRange : MonoBehaviour
{
    [SerializeField] float attackRange = 10f;
    public GameObject currentAimTarget;

    void Awake()
    {
        currentAimTarget = null;
        gameObject.transform.localScale = new Vector3(attackRange, attackRange, 1);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (currentAimTarget != null)
            return;
        if (!collision.gameObject.CompareTag("Enemy"))
            return;
        currentAimTarget = collision.gameObject;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == currentAimTarget)
            currentAimTarget = null;
    }
}
