using UnityEngine;

public class HazardScript : MonoBehaviour
{
    [Header("Damage")]
    public int damageAmount = 1;
    public float damageTime = 1;

    [Header("Knockback")]
    public bool dealKnockback = true;
    public float knockbackAmount = 1;

    [Header("Other")]
    public bool destroyOnTrigger = true;
}
