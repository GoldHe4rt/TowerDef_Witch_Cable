using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombatSystem : MonoBehaviour
{
    [SerializeField] public WeaponDatabaseSO databaseSO;
    [SerializeField] private GameObject weaponHolder;
    [SerializeField] private int playerID = 1;
    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private WeaponDisplay weaponDisplay;

    internal int currentWeaponID = -1;
    private GameObject currentWeaponPrefab;
    private GameObject currentHitboxPrefab;
    private float currentHitboxSpeed;
    private bool currentlyStickToWeapon;

    internal List<float> currentCoolDowns = new List<float>();

    void Start()
    {
        for (int i = 0; i < databaseSO.weaponData.Count; i++)
        {
            currentCoolDowns.Add(0);
        }
        NewWeapon(0);
    }

    void Update()
    {
        for (int i = 0; i < databaseSO.weaponData.Count; i++)
        {
            if (currentCoolDowns[i] > 0)
            {
                currentCoolDowns[i] -= Time.deltaTime;
            } else
            {
                currentCoolDowns[i] = 0;
            }
            
        }

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
        currentlyStickToWeapon = databaseSO.weaponData[newWeaponID].StickToWeapon;
        // Initialize combat system if needed
        weaponDisplay.UpdateSelectionDisplay(currentWeaponID);
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

    public void Attack()
    {
        if (currentWeaponID == -1)
        {
            return;
        }
        if (currentCoolDowns[currentWeaponID] > 0)
        {
            return;
        }
        // Implement attack logic here


        //Spawn Damage dealer
        GameObject currentAttack;
        currentAttack = Instantiate(currentHitboxPrefab, weaponHolder.transform.position, weaponHolder.transform.rotation);

        //Stick to weapon if needed and add velocity to it
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();
        if (!currentlyStickToWeapon)
        {
            // Add Velocity to Damage dealer and add or subtract playerspeed to it based on the direction of the attack and movement
            rb.linearVelocity = weaponHolder.transform.rotation * Vector2.up * currentHitboxSpeed;
            rb.linearVelocity += playerMovement.moveInput.normalized * playerMovement.moveSpeed * 0.5f;
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
        currentCoolDowns[currentWeaponID] = databaseSO.weaponData[currentWeaponID].AttackCooldown; // Reset the attack cooldown
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
        yield return new WaitForSeconds(currentCoolDowns[currentWeaponID]);
        currentCoolDowns[currentWeaponID] = 0;
        Attack();
    }
}
