using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponDatabaseSO : ScriptableObject
{
    public List<WeaponData> weaponData;
}

[Serializable]
public class WeaponData
{
    [field: SerializeField]
    public string Name { get; private set;}
    [field: SerializeField]
    public int ID { get; private set;}
    [field: SerializeField]
    public GameObject[] Prefab { get; private set;}
    [field: SerializeField]
    public float AttackCooldown { get; private set;} = 0f;
    [field: Header("Hitbox Settings")]
    [field: SerializeField]
    public GameObject[] HitboxPrefab { get; private set;}
    [field: SerializeField]
    public float HitboxSpeed { get; private set;} = 0f;
    [field: SerializeField]
    public float Lifetime { get; private set;} = 3f;
    [field: SerializeField]
    public bool StickToWeapon { get; private set;} = false;
    [field: Header("Lazer Settings")]
    [field: SerializeField]
    public bool IsLazer { get; private set;} = false;
    [field: SerializeField]
    public bool LockRotationOnAttack { get; private set;} = false;
    [field: SerializeField]
    public float LazerRange { get; private set;} = 5f;
    [field: Header("Support Settings")]
    [field: SerializeField]
    public bool IsSupport { get; private set;} = false;

}