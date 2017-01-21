﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public GameObject[] enemyList;
    private float spawnTimer;
    private float difficultyTimer;
    private int spawnCount;
    public float maxSpawnTime;
    public float minSpawnTime;
    public int maxPackSize;
    public int minPackSize;
    public int difficultyLevel;
    private int currentRotation;
    public float incrementDifficultyTime;

    // Use this for initialization
    void Start()
    {
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        difficultyTimer = 0;
        spawnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0 && spawnCount < (GameManager.Instance.currentLevel * 2))
        {
            for (int i = 0; i <= (int)Random.Range(minPackSize, maxPackSize); i++)
            {
                GameObject enemy = enemyList[(int)Random.Range(0, enemyList.Length)];
                Instantiate(enemy, new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y + 1, transform.position.z + Random.Range(-2, 2)), Quaternion.identity);
                spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            }
            spawnCount++;
        }
    }

    void IncrementDifficulty()
    {
        currentRotation++;

        if(GameManager.Instance.currentLevel % 1 == 0)
        {
            maxPackSize++;
        }
        else
        {
            minPackSize++;
        }
    }

    public void ResetSpawnCount()
    {
        spawnCount = 0;
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
