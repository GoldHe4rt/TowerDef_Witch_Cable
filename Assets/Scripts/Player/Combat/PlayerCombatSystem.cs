using System;
using UnityEngine;
using System.Collections;

public class PlayerCombatSystem : MonoBehaviour
{
    [SerializeField] public WeaponDatabaseSO databaseSO;
    [SerializeField] private GameObject weaponHolder;

    private int currentWeaponID = -1;
    private GameObject currentWeaponPrefab;
    private GameObject currentHitboxPrefab;
    private float currentHitboxSpeed;
    private float currentAttackSpeed;
    private float shootTimer;

    void Update()
    {
        if (shootTimer > 0)
            shootTimer -= Time.deltaTime;
        
    }

    private void NewWeapon(int newWeaponID)
    {
        RemoveWeapon();
        if (newWeaponID < 0 || newWeaponID >= databaseSO.weaponData.Count)
        {
            Debug.LogError("Invalid weapon ID");
            return;
        }
        currentWeaponID = newWeaponID;
        currentWeaponPrefab = Instantiate(databaseSO.weaponData[newWeaponID].Prefab, weaponHolder.transform.position, weaponHolder.transform.rotation);
        currentHitboxPrefab = databaseSO.weaponData[newWeaponID].HitboxPrefab;
        currentWeaponPrefab.transform.SetParent(weaponHolder.transform);
        currentHitboxSpeed = databaseSO.weaponData[newWeaponID].HitboxSpeed;
        currentAttackSpeed = databaseSO.weaponData[newWeaponID].AttackSpeed;
        // Initialize combat system if needed
    }

    private void RemoveWeapon()
    {
        currentWeaponID = -1;
        if (currentWeaponPrefab != null)
        {
            Destroy(currentWeaponPrefab);
        }
        currentWeaponPrefab = null;
        currentHitboxPrefab = null;
        Debug.Log("Weapon removed");
    }

    public void Attack()
    {
        if (currentWeaponID == -1)
        {
            Debug.LogWarning("No weapon equipped!");
            return;
        }
        if (shootTimer > 0)
        {
            Debug.Log("Weapon is on cooldown!");
            return;
        }
        // Implement attack logic here
        Debug.Log("Player is attacking!");

        //Spawn Damage dealer
        GameObject currentAttack;
        currentAttack = Instantiate(currentHitboxPrefab, weaponHolder.transform.position, weaponHolder.transform.rotation);

        //Add Velocity to Damage dealer
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();
        rb.linearVelocity = weaponHolder.transform.rotation * Vector2.up * currentHitboxSpeed;

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
            Debug.Log($"Wrapping around to the first weapon with ID {index}.");
        }
        NewWeapon(index);
    }

    public void DecreaseWeaponID()
    {
        int index = currentWeaponID - 1;
        if (0 > index)
        {
            index = databaseSO.weaponData.Count - 1; // Wrap around to the last weapon
            Debug.Log($"Wrapping around to the last weapon with ID {index}.");
        }
        NewWeapon(index);
    }
}
