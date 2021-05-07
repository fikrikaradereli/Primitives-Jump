using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private GameObject lastEnemy;
    private int enemyCount = 0;

    public static event Action LevelSuccessful;


    private readonly Vector3[] spawnPositions = { new Vector3(7f, 0.5f, 0), new Vector3(-7f, 0.5f, 0) };

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1, 1);
    }

    private void SpawnEnemy()
    {
        if (GameManager.Instance.CurrentGameState == GameState.RUNNING
            && enemyCount < GameManager.Instance.CurrentLevel.EnemyNumber)
        {
            int randomIndex = UnityEngine.Random.Range(0, spawnPositions.Length);
            Vector3 spawnPos = spawnPositions[randomIndex] + new Vector3(0, enemyCount, 0);

            lastEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);

            enemyCount++;
        }

        if (enemyCount == GameManager.Instance.CurrentLevel.EnemyNumber)
        {
            if (lastEnemy.GetComponent<EnemyController>().Speed == 0)
            {
                LevelSuccessful?.Invoke();
            }
        }
    }
}