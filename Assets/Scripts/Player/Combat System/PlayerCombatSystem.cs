using System;
using UnityEngine;
using System.Collections;

public class PlayerCombatSystem : MonoBehaviour
{
    [SerializeField] public WeaponDatabaseSO databaseSO;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private int playerID = 1;
    [SerializeField] private CurrencyManager currencyManager;

    internal int currentWeaponID = -1;
    private GameObject currentWeaponPrefab;
    private GameObject currentHitboxPrefab;
    private float currentHitboxSpeed;
    private float currentAttackSpeed;
    private bool currentlyStickToWeapon;

    private float shootTimer;

    void Start()
    {
        NewWeapon(0);
    }

    void Update()
    {
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;

    }

    public void NewWeapon(int newWeaponID)
    {
        RemoveWeapon();
        if (newWeaponID < 0 || newWeaponID >= databaseSO.weaponData.Count)
        {
            return;
        }
        currentWeaponID = newWeaponID;
        currentWeaponPrefab = Instantiate(databaseSO.weaponData[newWeaponID].Prefab, weaponHolder.transform.position, weaponHolder.transform.rotation);
        currentHitboxPrefab = databaseSO.weaponData[newWeaponID].HitboxPrefab;
        currentWeaponPrefab.transform.SetParent(weaponHolder.transform);
        currentHitboxSpeed = databaseSO.weaponData[newWeaponID].HitboxSpeed;
        currentAttackSpeed = databaseSO.weaponData[newWeaponID].AttackSpeed;
        currentlyStickToWeapon = databaseSO.weaponData[newWeaponID].StickToWeapon;
        // Initialize combat system if needed
    }

    public void RemoveWeapon()
    {
        currentWeaponID = -1;
        if (currentWeaponPrefab != null)
        {
            Destroy(currentWeaponPrefab);
        }
        currentWeaponPrefab = null;
        currentHitboxPrefab = null;
    }

    public void Attack(bool shortenTimer)
    {
        if (currentWeaponID == -1)
        {
            return;
        }
        if (shortenTimer ? shootTimer > currentAttackSpeed * 0.5f : shootTimer > 0)
        {
            return;
        }
        // Implement attack logic here


        //Spawn Damage dealer
        GameObject currentAttack;
        currentAttack = Instantiate(currentHitboxPrefab, weaponHolder.transform.position, weaponHolder.transform.rotation);

        //Add Velocity to Damage dealer
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();
        
        if (!currentlyStickToWeapon)
        {
            rb.linearVelocity = weaponHolder.transform.rotation * Vector2.up * currentHitboxSpeed;
        }
        else
        {
            Destroy(rb);
            currentAttack.transform.SetParent(weaponHolder.transform);
            currentAttack.transform.localPosition = Vector3.zero;
            currentAttack.transform.localRotation = Quaternion.identity;
        }

        //Set Owner of Attack
        DamageDealer damageDealer = currentAttack.GetComponent<DamageDealer>();
        damageDealer.playerOwner = playerID;
        damageDealer.currencyManager = currencyManager;

        //Destroy after set time
        StartCoroutine(DestroyHitboxAfterTime(currentAttack, databaseSO.weaponData[currentWeaponID].Lifetime));
        shootTimer = currentAttackSpeed; // Reset the attack cooldown
    }

    private IEnumerator DestroyHitboxAfterTime(GameObject currentAttack, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(currentAttack);
    }

    public void IncreaseWeaponID()
    {
        int index = currentWeaponID + 1;
        if (databaseSO.weaponData.Count <= index)
        {
            index = 0; // Wrap around to the first weapon
        }
        NewWeapon(index);
    }

    public void DecreaseWeaponID()
    {
        int index = currentWeaponID - 1;
        if (0 > index)
        {
            index = databaseSO.weaponData.Count - 1; // Wrap around to the last weapon
        }
        NewWeapon(index);
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(shootTimer);
        shootTimer = 0;
        Attack(false);
    }
}
