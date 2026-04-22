using System;
using UnityEngine;
[System.Serializable]
public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] Wave currentWave;
    [SerializeField] int currentWaveNumber;

    private void Update()
    {
        currentWave = waves[currentWaveNumber]
    }

    void SpawnWave()
    {
        GameObject randomEnemy = currentWave.typeOfEnemies[Random.range(0, currentWave.typeOfEnemies.Length)];
        Transform randomPoint =
    }
}
