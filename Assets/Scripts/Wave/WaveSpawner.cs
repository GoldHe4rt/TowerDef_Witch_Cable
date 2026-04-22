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

    [SerializeField]
    bool canSpawn = true;

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
        {
            currentWaveNumber++;
            canSpawn = true;
        }
    }

    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }

    void SpawnWave()
    {
        if (canSpawn)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
            }
        }

    }
}
