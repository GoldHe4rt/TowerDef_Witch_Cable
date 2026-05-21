using System;
using UnityEngine;
using System.Collections;

public enum TowerType
{
    Turret,
    Explosive
}

public class TowerAttack : MonoBehaviour
{
    [Header("Turret")]
    [SerializeField] public TowerType towerType = TowerType.Turret;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private GameObject attackRangeObject;
    [SerializeField] private float hitboxSpeed = 5f;
    [SerializeField] private float hitboxLifetime = 2f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float spreadAngle = 10f;

    private AttackRange attackRangeScript;
    private float attackSpeedTimer = 0f;
    private Vector2 currentAimDirectionTarget = Vector2.up;

    [Header("Player")]
    public int playerID = -1;
    internal int placedObjectIndex = -1;
    internal CurrencyManager currencyManager;

    void Start()
    {
        attackRangeScript = attackRangeObject.GetComponent<AttackRange>();
    }

    void Update()
    {
        if (attackSpeedTimer > 0)
            attackSpeedTimer -= Time.deltaTime;
        

        if (towerType == TowerType.Turret)
        {
            if (attackRangeScript.currentAimTarget != null)
            {
                Aim();
                if (attackSpeedTimer <= 0)
                {
                    Attack();
                }
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

        //Set the DamageDealer values
        DamageDealer damageDealer = currentAttack.GetComponent<DamageDealer>();
        damageDealer.playerOwner = playerID;
        damageDealer.currencyManager = currencyManager;

        //Add Velocity to Damage dealer
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();
        rb.linearVelocity = spread * hitboxSpeed;

        //Destroy after set time
        damageDealer.StartCoroutine(damageDealer.DestroyHitboxAfterTime(hitboxLifetime));
        attackSpeedTimer = attackSpeed; // Reset the attack cooldown
    }

    public void Explode()
    {
        Attack();
        DestroySelf();
    }

    private void DestroySelf()
    {
        if (placedObjectIndex >= 0 && GridPlacementManager.Instance != null)
            {
                GridPlacementManager.Instance.RemovePlacedObject(placedObjectIndex);
                placedObjectIndex = -1;
                return;
            }
        Destroy(gameObject);
    }
}
