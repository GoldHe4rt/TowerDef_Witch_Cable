using System;
using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] public bool canAttack = true;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private GameObject attackRangeObject;

    [Header("Difficulty Scaling")]
    [SerializeField] private float minHitboxSpeed = 3f;
    [SerializeField] private float maxHitboxSpeed = 10f;
    private float hitboxSpeed = 5f;
    [SerializeField] private float minAttackSpeed = 2f;
    [SerializeField] private float maxAttackSpeed = 0.5f;
    private float attackSpeed = 1f;

    [Header("Attack Settings")]
    [SerializeField] private float hitboxLifetime = 2f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float spreadAngle = 10f;
    [SerializeField] private int minCampDamage = 1;
    [SerializeField] private int maxCampDamage = 15;
    internal int campDamage = 1;

    private EnemyRange attackRangeScript;
    private float attackSpeedTimer = 0f;
    private Vector2 currentAimDirectionTarget = Vector2.up;

    
    void Start()
    {
        attackRangeScript = attackRangeObject.GetComponent<EnemyRange>();
    }
    
    void Update()
    {
        if (!canAttack)
            return;
        if (attackSpeedTimer > 0)
            attackSpeedTimer -= Time.deltaTime;

        if (attackRangeScript.currentAimTarget != null)
        {
            Aim();
            if (attackSpeedTimer <= 0)
            {
                Attack();
            }
        }
        
    }

    private void Aim()
    {
        currentAimDirectionTarget = attackRangeScript.currentAimTarget.transform.position - transform.position;
        currentAimDirectionTarget.Normalize();
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, currentAimDirectionTarget);
        weaponHolder.transform.rotation = Quaternion.RotateTowards(weaponHolder.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    public void Attack()
    {
        if (attackSpeedTimer > 0)
        {
            return;
        }
        // Implement attack logic here

        //Set Aim Direction
        Vector2 currentAimDirection = weaponHolder.transform.rotation * Vector2.up;
        Quaternion spreadRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-spreadAngle, spreadAngle));
        Vector2 spread = spreadRotation * currentAimDirection;

        //Spawn Damage dealer
        GameObject currentAttack;
        currentAttack = Instantiate(hitboxPrefab, weaponHolder.transform.position, weaponHolder.transform.rotation * spreadRotation);

        //Add Velocity to Damage dealer
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();
        rb.linearVelocity = spread * hitboxSpeed;

        //Destroy after set time
        Hazard hazard = currentAttack.GetComponent<Hazard>();
        hazard.StartCoroutine(hazard.DestroyHitboxAfterTime(hitboxLifetime));
        attackSpeedTimer = attackSpeed; // Reset the attack cooldown
    }

    internal void SetDifficultyModifier(float difficultyModifier)
    {
        hitboxSpeed = Mathf.Lerp(minHitboxSpeed, maxHitboxSpeed, difficultyModifier);
        attackSpeed = Mathf.Lerp(minAttackSpeed, maxAttackSpeed, difficultyModifier);
        campDamage = Mathf.RoundToInt(Mathf.Lerp(minCampDamage, maxCampDamage, difficultyModifier));
    }
}
