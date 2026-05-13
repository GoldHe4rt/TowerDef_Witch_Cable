

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
}

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;
    public Animator anim;
    public TMP_Text waveName;

    private Wave currentWave;
    private int currentWaveNumber;

    private bool canSpawn = true;
    private bool canAnimate = false;
    private float nextSpawnTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log("1");
        if (totalEnemies.Length == 0 && currentWaveNumber + 1 != waves.Length && canAnimate)
        {
            waveName.text = waves[currentWaveNumber + 1].waveName;
            anim.SetBool("WaveComplete", canAnimate);
            canSpawn = true;
            canAnimate = false;
            Debug.Log("2");

        }
    }

    void SpawnNextWave()
    {
        currentWaveNumber++;
        Debug.Log("3");

    }

    void SpawnWave()
    {
        Debug.Log("4");
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject randomEnemy = currentWave.typeOfEnemies[Random.Range(0, currentWave.typeOfEnemies.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject cloneObject = Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);
            cloneObject.SetActive(true);
            currentWave.noOfEnemies--;
            nextSpawnTime = Time.time + currentWave.spwanInterval;
            Debug.Log("5");
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
                Debug.Log("6");
            }
        }
    }




}