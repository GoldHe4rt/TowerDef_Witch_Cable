

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
[System.Serializable]

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject[] typeOfEnemies;
    public float spwanInterval;
    [Range(0.1f, 10f)] public float difficultyModifier = 1f;
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
            Debug.Log("GameFinish");
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
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            EnemySpawner randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            randomPoint.Spawn(randomEnemy);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + Random.Range(currentWave.spwanInterval - currentWave.spwanInterval * 0.1f, currentWave.spwanInterval + currentWave.spwanInterval * 0.1f);
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }




}