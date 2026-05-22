using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour
{
    [Header("Damage")]
    public int damageAmount = 1;
    public float damageTime = 1f;

    [Header("Knockback")]
    public bool dealKnockback = true;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;
    public float stunDuration = 0.2f;


    [Header("Other")]
    public int pierceAmount = 1;

    public void HitTarget()
    {
        pierceAmount--;
        if (pierceAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator DestroyHitboxAfterTime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
