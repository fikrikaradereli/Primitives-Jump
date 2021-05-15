using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private EnemyController lastEnemy;
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

            lastEnemy = Instantiate(enemy, spawnPos, Quaternion.identity).GetComponent<EnemyController>();
            //lastEnemy.GetComponent<MeshRenderer>().material.color = GameManager.Instance.CurrentLevel.EnemyColor;

            enemyCount++;

            if (enemyCount > (GameManager.Instance.CurrentLevel.EnemyNumber / 2))
            {
                float minSpeed = GameManager.Instance.CurrentLevel.minEnemySpeed;
                float maxSpeed = GameManager.Instance.CurrentLevel.maxEnemySpeed;

                lastEnemy.speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
            }
            else
            {
                lastEnemy.speed = GameManager.Instance.CurrentLevel.minEnemySpeed;
            }
        }

        if (enemyCount == GameManager.Instance.CurrentLevel.EnemyNumber)
        {
            if (lastEnemy.speed == 0)
            {
                LevelSuccessful?.Invoke();
                CancelInvoke(nameof(SpawnEnemy));
            }
        }
    }
}