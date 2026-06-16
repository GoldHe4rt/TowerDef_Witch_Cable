using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] internal int damageAmount = 1;
    [SerializeField] internal float hurtTime = 1f;

    [Header("Knockback")]
    internal bool dealKnockback = true;
    internal float knockbackForce = 10f;
    internal float knockbackDuration = 0.2f;
    internal float stunDuration = 0.2f;

    [Header("Freeze")]
    [SerializeField] internal bool freezeTarget = false;
    [SerializeField] internal float freezeDuration = 5f;

    [Header("Other")]
    [SerializeField] internal int playerOwner = -1;
    [SerializeField] internal int pierceAmount = 1;

    internal CurrencyManager currencyManager;

    public void HitTarget()
    {
        if (pierceAmount == -1) return; //-1 is infinite
        pierceAmount--;
        if (pierceAmount == 0)
        {
            Destroy(gameObject);
        }
    }

    public void KilledTarget(int currency)
    {
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(currency, playerOwner, true);
        }
    }

    public IEnumerator DestroyHitboxAfterTime(float lifetime, float damageTimer)
    {
        yield return new WaitForSeconds(damageTimer);
        damageAmount = 0;
        yield return new WaitForSeconds(lifetime - damageTimer);
        Destroy(gameObject);
    }
}
