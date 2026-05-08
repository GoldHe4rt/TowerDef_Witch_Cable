using UnityEngine;

public class HazardScript : MonoBehaviour
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
    public bool destroyOnTrigger = true;
}
