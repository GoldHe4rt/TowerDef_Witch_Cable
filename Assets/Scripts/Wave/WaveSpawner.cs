using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
[System.Serializable]

public class Wave
{
    [SerializeField] public string waveName;
    [SerializeField] public int noOfEnemies;
    [SerializeField] public GameObject[] typeOfEnemies;
    [SerializeField] public float spawnInterval;
}

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public Wave[] waves;
    [SerializeField] public Transform[] spawnPoints;
    [SerializeField] public Animator animator;
    [SerializeField] public TMP_Text waveName;

    [SerializeField] private Wave currentWave;
    [SerializeField] private int currentWaveNumber;
    [SerializeField] private float nextSpawnTime;

    [SerializeField] bool canSpawn = true;
    [SerializeField] private bool canAnimate = false;

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0)
        {
            if (currentWaveNumber + 1 != waves.Length)
            {
                if (canAnimate)
                {
                    waveName.text = waves[currentWaveNumber + 1].waveName;
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            }
            else
            {
                Debug.Log("GameFinish");
            }
        }
    }

    void SpawnNextWave()
    {
        currentWaveNumber++;
        canSpawn = true;
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }

    }
}
