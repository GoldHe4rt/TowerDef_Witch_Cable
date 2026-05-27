using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyDatabaseSO : ScriptableObject
{
    public List<EnemyData> enemyData;
}

[Serializable]
public class EnemyData
{
    [field: SerializeField]
    public string Name { get; private set;}
    [field: SerializeField]
    public int ID { get; private set;}
    [field: SerializeField]
    public GameObject Prefab { get; private set;}
    [field: SerializeField]
    public Vector2 SizeRange { get; private set;} = new Vector2(0.75f, 1.25f);
    [field: SerializeField]
    public Vector2 HealthRange { get; private set;} = new Vector2(2f, 7f);
    [field: SerializeField]
    public Vector2 MovementRange { get; private set;} = new Vector2(3f, 5f);

}