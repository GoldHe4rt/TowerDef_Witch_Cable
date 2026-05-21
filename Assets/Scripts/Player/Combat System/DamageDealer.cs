using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour
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
    public int playerOwner = -1;
    public int pierceAmount = 1;

    internal CurrencyManager currencyManager;

    public void HitTarget()
    {
        pierceAmount--;
        if (pierceAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void KilledTarget(int currency)
    {
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(currency, playerOwner);
        }
    }

    public IEnumerator DestroyHitboxAfterTime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
