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
    [SerializeField] private GlobalReferanceManager globalReferanceManager;
    [SerializeField] private WeaponDisplay weaponDisplay;

    internal bool shieldActive = false;

    internal int currentWeaponID = -1;
    private GameObject currentWeaponPrefab;
    private GameObject currentHitboxPrefab;
    private GameObject currentLaserAttack;
    private float currentHitboxSpeed;
    private float currentLifetime;
    private bool currentlyStickToWeapon;
    private bool currentlyIsLazer;
    private bool currentlyLockRotation;
    private float currentlyLazerRange;
    internal List<float> currentCoolDowns = new List<float>();

    RaycastHit2D hit;
    LayerMask lazerLayerMask;

    void Start()
    {
        lazerLayerMask = LayerMask.GetMask("Wall", "Character");
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
            if (currentCoolDowns[i] > -1)
            {
                currentCoolDowns[i] -= Time.deltaTime;
            } else
            {
                currentCoolDowns[i] = -1;
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
        currentWeaponPrefab = Instantiate(databaseSO.weaponData[newWeaponID].Prefab[playerID-1], weaponHolder.transform.position, weaponHolder.transform.rotation);
        currentWeaponPrefab.transform.SetParent(weaponHolder.transform);
        currentLifetime = databaseSO.weaponData[newWeaponID].Lifetime;
        currentlyStickToWeapon = databaseSO.weaponData[newWeaponID].StickToWeapon;
        currentlyIsLazer = databaseSO.weaponData[newWeaponID].IsLazer;
        currentHitboxPrefab = databaseSO.weaponData[newWeaponID].HitboxPrefab[playerID-1];
        currentHitboxSpeed = databaseSO.weaponData[newWeaponID].HitboxSpeed;
        currentlyLockRotation = databaseSO.weaponData[newWeaponID].LockRotationOnAttack;
        currentlyLazerRange = databaseSO.weaponData[newWeaponID].LazerRange;

        
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
        currentlyStickToWeapon = false;
        currentlyIsLazer = false;
        currentHitboxPrefab = null;
        currentHitboxSpeed = 0;
        currentlyLockRotation = false;
        currentlyLazerRange = 0;
    }

    public void Attack(bool shortenTimer)
    {
        if (currentWeaponID == -1)
        {
            return;
        }
        if (shortenTimer ? currentCoolDowns[currentWeaponID] > 0 : currentCoolDowns[currentWeaponID] > -0.3f)
        {
            return;
        }
        
        // Implement attack logic here

        //Spawn Damage dealer
        GameObject currentAttack;
        currentAttack = Instantiate(currentHitboxPrefab, weaponHolder.transform.position, weaponHolder.transform.rotation);
        Rigidbody2D rb = currentAttack.GetComponent<Rigidbody2D>();

        //Stick to weapon if needed and add velocity to it
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

        

        if (currentlyLockRotation)
        {
            playerMovement.LockRotation = true;
        }

        if (currentlyIsLazer)
        {
            currentLaserAttack = currentAttack;
            foreach (Transform child in currentLaserAttack.transform)
            {
                DamageDealer damageDealer = child.GetComponent<DamageDealer>();
                if (damageDealer != null)
                {
                    damageDealer.playerOwner = playerID;
                    damageDealer.currencyManager = currencyManager;
                }
                
            }
        }
        else
        {
            //Set Owner of Attack
            DamageDealer damageDealer = currentAttack.GetComponent<DamageDealer>();
            if (damageDealer != null)
            {
                damageDealer.playerOwner = playerID;
                damageDealer.currencyManager = currencyManager;
            }
        }

        if (databaseSO.weaponData[currentWeaponID].IsSupport)
        {
            shieldActive = true;
        }

        //Destroy after set time
        StartCoroutine(DestroyHitboxAfterTime(currentAttack, currentlyLockRotation));

        if (globalReferanceManager.soundManager != null)
            globalReferanceManager.soundManager.PlayPlayerAttackSound();
        
        currentCoolDowns[currentWeaponID] = databaseSO.weaponData[currentWeaponID].AttackCooldown; // Reset the attack cooldown
    }

    void FixedUpdate()
    {
        if (currentLaserAttack == null)
            return;

        float rayDistance = currentlyLazerRange;
        hit = Physics2D.Raycast(weaponHolder.transform.position, weaponHolder.transform.TransformDirection(Vector2.up), currentlyLazerRange, lazerLayerMask);

        if (hit)
        {
            rayDistance = hit.distance;
            Debug.DrawRay(weaponHolder.transform.position, weaponHolder.transform.TransformDirection(Vector2.up) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(weaponHolder.transform.position, weaponHolder.transform.TransformDirection(Vector2.up) * currentlyLazerRange, Color.white);
        }

        UpdateLaserVisuals(currentLaserAttack, rayDistance);
    }

    private void UpdateLaserVisuals(GameObject laser, float length)
    {
        if (laser == null)
            return;

        Transform lazerStart = laser.transform.Find("Start");
        Transform lazerMiddle = laser.transform.Find("Middle");
        Transform lazerEnd = laser.transform.Find("End");

        if (lazerStart == null || lazerMiddle == null || lazerEnd == null)
            return;

        lazerStart.localPosition = Vector2.zero;
        lazerEnd.localPosition = Vector2.up * length;
        lazerMiddle.localPosition = Vector2.up * (length * 0.5f);
        lazerMiddle.localScale = new Vector2(lazerMiddle.localScale.x, Mathf.Max(0.001f, length));
    }

    private IEnumerator DestroyHitboxAfterTime(GameObject currentAttack, bool lockedRotation)
    {
        yield return new WaitForSeconds(currentLifetime);
        if (lockedRotation)
        {
            playerMovement.LockRotation = false;
        }

        if (currentAttack == currentLaserAttack)
        {
            currentLaserAttack = null;
        }

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
}
