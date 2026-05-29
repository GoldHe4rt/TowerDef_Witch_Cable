using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using Menu;

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
    public List<Wave> waves;
    public EnemySpawner[] spawnPoints;

    public Animator anim;
    public TMP_Text waveName;
    public WinMenuManager winMenuManager;
    [SerializeField] private GameObject[] totalEnemies;

    private Wave currentWave;
    private int currentWaveNumber;

    [Header("Endless Mode")]
    [SerializeField] private bool endlessMode = false;
    [SerializeField] private GameObject[] endlessEnemyPrefabs;
    [SerializeField] private int endlessDifficulty = 0;

    private bool canSpawn = false;
    private bool canAnimate = false;
    private float nextSpawnTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        if (endlessMode)
        {
            ActivateEndlessMode();
        }
        WaveAnim();
    }


    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && canAnimate)
        {
            if (currentWaveNumber + 1 != waves.Count)
            {
                currentWaveNumber++;
                WaveAnim();
            }
            else if (currentWaveNumber + 1 == waves.Count)
            {
                if (endlessMode)
                {
                    CreateNewEndlessWave();
                }
                else
                {
                    winMenuManager.Win();
                    Debug.Log("GameFinish");
                }
            }
            else
            {
                //Wave is in progress
            }
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

    public void ActivateEndlessMode()
    {
        endlessMode = true;
        waves.RemoveAll(w => true);
        currentWaveNumber = -1;
        CreateNewEndlessWave();
        currentWaveNumber = 0;
    }

    public void CreateNewEndlessWave()
    {
        //Create new Wave
        Wave newWave = new Wave();
        newWave.waveName = "Endless Wave " + (currentWaveNumber + 1);
        newWave.enemySettings = new List<EnemySettings>();
        for (int i = 0; i < endlessEnemyPrefabs.Length; i++)
        {
            var prefab = endlessEnemyPrefabs[i];
            newWave.enemySettings.Add(new EnemySettings
            {
                typeOfEnemy = prefab,
                noOfEnemy = Random.Range(endlessDifficulty * (endlessEnemyPrefabs.Length - i), endlessDifficulty * (endlessEnemyPrefabs.Length - i) * 2) / 2 + 1
            });
        }
        newWave.spwanInterval = Mathf.Lerp(1f, 0.1f, (float)endlessDifficulty / 100f);
        newWave.difficultyRangeActive = true;
        newWave.difficultyModifier = endlessDifficulty;

        //Set new Wave
        waves.Add(newWave);

        //Prepare next Wave
        endlessDifficulty += 5;
    }
}