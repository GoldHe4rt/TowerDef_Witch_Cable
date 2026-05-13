using UnityEngine;

public class HealingObject : MonoBehaviour
{
    [Header("Healing")]
    public int healAmount = 1;
    public float healTime = 1f;

    [Header("Other")]
    public bool destroyOnTrigger = false;
}
