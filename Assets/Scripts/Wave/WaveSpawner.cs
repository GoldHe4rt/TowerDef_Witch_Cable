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
    public EnemyType typeOfEnemy;
    internal GameObject enemyPrefab;
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

public enum GameDifficulty
{ 
    Easy,
    Normal,
    Hard,
    Nightmare,
    Endless
}

public enum EnemyType
{ 
    Light,
    Medium,
    Heavy
}

[System.Serializable]
public class WaveDifficultys
{
    public List<Wave> easyWaves;
    public List<Wave> normalWaves;
    public List<Wave> hardWaves;
    public List<Wave> nightmareWaves;
}

public class WaveSpawner : MonoBehaviour
{
    [Header("Difficulty")]
    public GameDifficulty difficulty;
    [SerializeField] private WaveDifficultys waveDifficultys;
    
    internal List<Wave> waves = new List<Wave>();

    [Header("Other")]
    [SerializeField] private Transform target;
    [SerializeField] private EnemySpawner[] spawnPoints;
    [SerializeField] private EnemyDatabaseSO databaseSO;
    [SerializeField] private GlobalReferanceManager globalReferenceManager;
    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text waveName;
    [SerializeField] private WinMenuManager winMenuManager;
    [SerializeField] private GameObject[] totalEnemies;

    private Wave currentWave;
    private int currentWaveNumber;

    [Header("Endless Mode")]
    [SerializeField] private bool endlessMode = false;
    private List<GameObject> endlessEnemyPrefabs = new List<GameObject>();
    [SerializeField] private int endlessDifficulty = 0;
    private float newEndlessDifficulty;

    private bool canSpawn = false;
    private bool canAnimate = false;
    private float nextSpawnTime;
    private bool sentErrorMessage = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        endlessMode = false;
        if (globalReferenceManager == null)
            Debug.LogWarning("GlobalReferanceManager reference is not assigned in WaveSpawner.");
        UpdateDifficulty();
        if (endlessMode)
        {
            PrepareEndlessEnemyPrefabs();
            ActivateEndlessMode();
        }
        WaveAnim();
    }

    private void Update()
    {
        if (currentWaveNumber >= waves.Count)
        {
            WavesMissingError();
            return;
        }
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
        if (currentWaveNumber >= waves.Count)
        {
            WavesMissingError();
            return;
        }
        globalReferenceManager.waveDisplay.text = (currentWaveNumber + 1).ToString("0");
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
            GameObject randomEnemyObject = currentWave.enemySettings[randomEnemy].enemyPrefab;
            EnemySpawner randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            randomPoint.Spawn(
                randomEnemyObject,
                target,
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

    private void WavesMissingError()
    {
        if (!sentErrorMessage)
        {
            Debug.LogError("Wave ID " + currentWaveNumber + " not found in " + difficulty + " difficulty. There are only " + waves.Count + " waves defined. Please add more wave data or change to a different difficulty in the DataManager since it overwrites existing data.");
            sentErrorMessage = true;
        }
    }

    public void UpdateDifficulty()
    {
        switch (difficulty)
        {
            case GameDifficulty.Easy:
                waves = waveDifficultys.easyWaves;
                break;
            case GameDifficulty.Normal:
                waves = waveDifficultys.normalWaves;
                break;
            case GameDifficulty.Hard:
                waves = waveDifficultys.hardWaves;
                break;
            case GameDifficulty.Nightmare:
                waves = waveDifficultys.nightmareWaves;
                break;
            case GameDifficulty.Endless:
                endlessMode = true;
                break;
        }
        PrepareEnemyPrefabs();
    }

    private void PrepareEnemyPrefabs()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            for (int j = 0; j < waves[i].enemySettings.Count; j++)
            {
                switch (waves[i].enemySettings[j].typeOfEnemy)
                {
                    case EnemyType.Light:
                        waves[i].enemySettings[j].enemyPrefab = databaseSO.enemyData[0].Prefab;
                        break;
                    case EnemyType.Medium:
                        waves[i].enemySettings[j].enemyPrefab = databaseSO.enemyData[1].Prefab;
                        break;
                    case EnemyType.Heavy:
                        waves[i].enemySettings[j].enemyPrefab = databaseSO.enemyData[2].Prefab;
                        break;
                }
            }
        }
    }

    private void PrepareEndlessEnemyPrefabs()
    {
        for (int i = 0; i < databaseSO.enemyData.Count; i++)
        {
            endlessEnemyPrefabs.Add(databaseSO.enemyData[i].Prefab);
        }
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
        for (int i = 0; i < endlessEnemyPrefabs.Count; i++)
        {
            var prefab = endlessEnemyPrefabs[i];
            newWave.enemySettings.Add(new EnemySettings
            {
                enemyPrefab = prefab,
                noOfEnemy = Random.Range(endlessDifficulty * (endlessEnemyPrefabs.Count - i), endlessDifficulty * (endlessEnemyPrefabs.Count - i) * 2) / 2 + 1
            });
        }
        newWave.spwanInterval = Mathf.Lerp(1f, 0.1f, (float)endlessDifficulty / 100f);
        newWave.difficultyRangeActive = true;
        newWave.difficultyModifier = endlessDifficulty;

        //Set new Wave
        waves.Add(newWave);

        //Prepare next Wave
        newEndlessDifficulty = newEndlessDifficulty * 1.20f + 4;
        endlessDifficulty = (int)newEndlessDifficulty;
    }

    /*/
    [SerializeField] private GameObject graph;
    [SerializeField] private GameObject graph2;

    private void Graph()
    {
        float graphValue = 0;
        int graphValue2 = 0;

        for (int i = 0; i < 10; i++) // Example: Create 10 initial endless waves
        {
            Instantiate(graph, graph.transform.position, graph.transform.rotation);
            graphValue = graphValue * 1.20f + 4;
            graph.transform.position = new Vector2(graph.transform.position.x + 10, 100 + graphValue);
            
            
            Instantiate(graph2, graph2.transform.position, graph2.transform.rotation);
            graphValue2 = graphValue2 + 5;
            graph2.transform.position = new Vector2(graph2.transform.position.x + 10, 100 + graphValue2);
            
        }
    }
    /*/
}