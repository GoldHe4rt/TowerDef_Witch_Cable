

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

[System.Serializable]
public class EnemySettings
{
    public GameObject typeOfEnemy;
    public int noOfEnemy;
}
[System.Serializable]
public class Wave
{
    public string waveName;
    public List<EnemySettings> enemySettings;
    public float spwanInterval;
    public bool difficultyRangeActive = false;
    [Range(1, 100)] public int difficultyModifier = 10;
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public EnemySpawner[] spawnPoints;
    public Animator anim;
    public TMP_Text waveName;

    private Wave currentWave;
    private int currentWaveNumber;



    private bool canSpawn = false;
    private bool canAnimate = false;
    private float nextSpawnTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        WaveAnim();
    }


    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && currentWaveNumber + 1 != waves.Length && canAnimate)
        {
            currentWaveNumber++;
            WaveAnim();
        }
        else
        {
            //Debug.Log("GameFinish");
        }
    }

    void WaveAnim()
    {
        Debug.Log("animate wave ting");
        waveName.text = waves[currentWaveNumber].waveName;
        anim.SetTrigger("WaveComplete");
        canAnimate = false;
    }


    public void SpawnNextWave()
    {
        canSpawn = true;
        Debug.Log("spawn next wave");
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            int randomEnemy = GetRandomEnemy();

            if (currentWave.enemySettings[randomEnemy].noOfEnemy <= 0)
            {
                currentWave.enemySettings.RemoveAt(randomEnemy);
                if (currentWave.enemySettings.Count == 0)
                {
                    canSpawn = false;
                    canAnimate = true;
                }
                return;
            }

            GameObject randomEnemyObject = currentWave.enemySettings[randomEnemy].typeOfEnemy;
            EnemySpawner randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            randomPoint.Spawn(
                randomEnemyObject, 
                currentWave.difficultyModifier, 
                currentWave.difficultyRangeActive);
            currentWave.enemySettings[randomEnemy].noOfEnemy--;
            nextSpawnTime = Time.time + Random.Range(
                currentWave.spwanInterval - currentWave.spwanInterval * 0.3f, 
                currentWave.spwanInterval + currentWave.spwanInterval * 0.3f);
        }
    }

    public int GetRandomEnemy() 
    {
        float totalEnemyCount = 0;
        
        foreach (var enemy in currentWave.enemySettings) totalEnemyCount += enemy.noOfEnemy; // Sum all enemy counts

        float randomValue = Random.Range(0, totalEnemyCount); // Pick random within total
        float cumulativeWeight = 0;

        foreach (var enemy in currentWave.enemySettings) {
            cumulativeWeight += enemy.noOfEnemy;
            if (randomValue <= cumulativeWeight) { // Select enemy when threshold is met
                return currentWave.enemySettings.IndexOf(enemy);
            }
        }
        return -1;
    }
}