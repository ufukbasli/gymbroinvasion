using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int waveCurrency;
    public Transform spawnLocation;
    public float spawnInterval;
    public float timeBtwWaves;
    public float startDelay;
    public int waveCount;

    private int currentWaveCount=0;
    private int waveCredit;
    private List<GameObject> generatedEnemies = new List<GameObject>();
    private void Awake()
    {
        waveCredit = waveCurrency;

    }
    private void Start()
    {
        Invoke("GenerateEnemyList", startDelay);

    }
    private void GenerateEnemyList()
    {
        if (waveCredit > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveCredit - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyId].enemyPrefab);

                waveCredit -= randEnemyCost;
            }

            Invoke("GenerateEnemyList",0);

        }
        else
        {
            CreateEnemies();
        }
    }

    private void CreateEnemies()
    {
        if (currentWaveCount >= waveCount) return;
        if (generatedEnemies.Count > 0)
        {
            Instantiate(generatedEnemies[0], spawnLocation.position, Quaternion.identity);
            generatedEnemies.RemoveAt(0);
            
            Invoke("CreateEnemies", spawnInterval);
            
        }
        else
        {
            Invoke("GenerateEnemyList", timeBtwWaves);
            currentWaveCount++;
            waveCredit = waveCurrency;


        }
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}


