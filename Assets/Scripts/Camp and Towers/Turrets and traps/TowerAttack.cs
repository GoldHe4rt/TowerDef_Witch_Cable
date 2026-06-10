using System;
using UnityEngine;
using System.Collections;
using TMPro;

public enum TowerType
{
    Turret,
    Explosive,
    Spike,
    Freeze,
    Barricade
}

public class TowerAttack : MonoBehaviour
{
    [Header("Tower Settings")]
    [SerializeField] public TowerType towerType = TowerType.Turret;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private GameObject mainGameObject;
    [SerializeField] private Animator animator;

    [Header("Attack Settings")]
    [SerializeField] private GameObject attackRangeObject;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackDelay = 0f;
    [SerializeField] private float rotationSpeed = 500f;
    [SerializeField] private float spreadAngle = 10f;
    private AttackRange attackRangeScript;
    private float attackSpeedTimer = 0f;
    private Vector2 currentAimDirectionTarget = Vector2.up;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth = 100;
    [SerializeField] private bool DamageIFramesEnabled = true;
    [SerializeField] private float damageFrames = 1f;
    private bool canTakeDamage = true;


    [Header("Bullet Settings")]
    [SerializeField] private TextMeshProUGUI bulletCountText;
    [SerializeField] private int maxBulletAmount = 10;
    private int bulletAmount = 10;

    [Header("Hitbox Settings")]
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private float hitboxSpeed = 5f;
    [SerializeField] private float hitboxLifetime = 2f;
    [SerializeField] private float hitboxDamageTime = 1f;

    [Header("Player")]
    public int playerID = -1;
    internal int placedObjectIndex = -1;
    internal CurrencyManager currencyManager;

    void Start()
    {
        currentHealth = maxHealth;
        attackRangeScript = attackRangeObject.GetComponent<AttackRange>();
        bulletAmount = maxBulletAmount;
        if (towerType != TowerType.Barricade)
        {
            bulletCountText.text = bulletAmount.ToString();
        } else
        {
            bulletCountText.text = currentHealth.ToString();
        }
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

        //Start Attack Animation
        if (animator != null)
        animator.SetTrigger("Activate");

        //Spawn DamageDealer
        StartCoroutine(SpawnDamageDealer());

        // Reset the attack cooldown
        attackSpeedTimer = attackSpeed; 
        
    }

    IEnumerator SpawnDamageDealer()
    {
        yield return new WaitForSeconds(attackDelay);

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
        damageDealer.StartCoroutine(damageDealer.DestroyHitboxAfterTime(hitboxLifetime, hitboxDamageTime));
    
        //Update bullets where -1 is infinite
        if (bulletAmount != -1)
        {
            bulletAmount--;
            bulletCountText.text = bulletAmount.ToString();
            if (bulletAmount <= 0)
                DestroySelf();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (canTakeDamage == false) return;
        currentHealth -= damageAmount;
        bulletCountText.text = currentHealth.ToString();
        //Debug.Log("Took " + damageAmount + " Damage!");

        if (currentHealth <= 0)
        {
            DestroySelf();
            return;
        }

        if (DamageIFramesEnabled == true)
        {
            canTakeDamage = false;
            StartCoroutine(HurtIFrames(damageFrames));
        }
            
    }

        private IEnumerator HurtIFrames(float damageFrames)
    {
        yield return new WaitForSeconds(damageFrames);
        canTakeDamage = true;
    }

    public void Explode()
    {
        bulletAmount = 1;
        Attack();
    }

    private void DestroySelf()
    {
        if (placedObjectIndex >= 0 && GridPlacementManager.Instance != null)
            {
                GridPlacementManager.Instance.RemovePlacedObject(placedObjectIndex);
                placedObjectIndex = -1;
                return;
            }
        Destroy(mainGameObject);
    }

    internal void Freeze()
    {
        throw new NotImplementedException();
    }
}
