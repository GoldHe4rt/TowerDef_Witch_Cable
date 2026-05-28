using UnityEngine;
using System.Collections;

public class DamageDealer : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] internal int damageAmount = 1;
    [SerializeField] internal float damageTime = 1f;

    [Header("Knockback")]
    internal bool dealKnockback = true;
    internal float knockbackForce = 10f;
    internal float knockbackDuration = 0.2f;
    internal float stunDuration = 0.2f;

    [Header("Other")]
    [SerializeField] internal int playerOwner = -1;
    [SerializeField] internal int pierceAmount = 1;

    internal CurrencyManager currencyManager;

    private void Start()
    {
        StartCoroutine(StopDamageAfterTime());
    }

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

    private IEnumerator StopDamageAfterTime()
    {
        yield return new WaitForSeconds(damageTime);
        damageAmount = 0;
    }

    public IEnumerator DestroyHitboxAfterTime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
