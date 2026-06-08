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
    public GameObject Prefab { get; private set;}
    [field: SerializeField]
    public GameObject HitboxPrefab { get; private set;}
    [field: SerializeField]
    public float HitboxSpeed { get; private set;} = 0f;
    [field: SerializeField]
    public float AttackCooldown { get; private set;} = 0f;
    [field: SerializeField]
    public float Lifetime { get; private set;} = 3f;
    [field: SerializeField]
    public bool StickToWeapon { get; private set;} = false;

}