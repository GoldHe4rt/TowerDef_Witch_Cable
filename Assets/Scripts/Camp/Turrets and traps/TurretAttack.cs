using System;
using UnityEngine;
using System.Collections;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private GameObject weaponHolder;
    public int campDamage = 1;

    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private float hitboxSpeed = 5f;
    [SerializeField] private float hitboxLifetime = 2f;
    [SerializeField] private float attackSpeed = 1f;
    private float attackSpeedTimer;
    

    void Update()
    {
        if (attackSpeedTimer > 0)
            attackSpeedTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.I))
        {
            Attack();
            Debug.Log("Key I pressed in TurretAttack"); //Temp test log
        }
    }

    public void Attack()
    {
        if (attackSpeedTimer > 0)
        {
            return;
        }
        // Implement attack logic here


        //Spawn Damage dealer
        GameObject currentAttack;
        currentAttack = Instantiate(hitboxPrefab, weaponHolder.transform.position, weaponHolder.transform.rotation);

        //Add Velocity to Damage dealer
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();
        rb.linearVelocity = weaponHolder.transform.rotation * Vector2.up * hitboxSpeed;

        //Destroy after set time
        StartCoroutine(DestroyHitboxAfterTime(currentAttack, hitboxLifetime));
        attackSpeedTimer = attackSpeed; // Reset the attack cooldown
    }

    private IEnumerator DestroyHitboxAfterTime(GameObject currentAttack, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(currentAttack);
    }


}
