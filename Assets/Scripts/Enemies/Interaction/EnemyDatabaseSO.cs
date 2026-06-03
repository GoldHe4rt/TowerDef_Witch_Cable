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
    public GameObject Prefab { get; private set;}

}