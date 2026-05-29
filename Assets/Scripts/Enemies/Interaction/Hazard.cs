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

    [Header("Spawn on Destroy")]
    [SerializeField] private bool spawnNewHazard = false;
    [SerializeField] private GameObject spawnOnDestroy;
    [SerializeField] private float newHazardSpeed = 5f;
    [SerializeField] private float newHazardLifetime = 2f;

    [Header("Other")]
    public int pierceAmount = 1;
    

    public void HitTarget()
    {
        pierceAmount--;
        if (pierceAmount <= 0)
        {
            DestroySelf();
        }
    }

    public IEnumerator DestroyHitboxAfterTime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        DestroySelf();
    }

    private void DestroySelf()
    {
        if (spawnNewHazard)
            Attack();
        Destroy(gameObject);
    }

    public void Attack()
    {

        //Set Aim Direction
        Vector2 currentAimDirection = transform.rotation * Vector2.up;

        //Spawn Damage dealer
        GameObject currentAttack;
        currentAttack = Instantiate(spawnOnDestroy, transform.position, transform.rotation);

        //Add Velocity to Damage dealer
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();
        rb.linearVelocity = currentAimDirection * newHazardSpeed;

        //Destroy after set time
        Hazard hazard = currentAttack.GetComponent<Hazard>();
        hazard.StartCoroutine(hazard.DestroyHitboxAfterTime(newHazardLifetime));
    }
}
