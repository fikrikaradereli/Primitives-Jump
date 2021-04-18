using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    private Vector3[] spawnPositions = { new Vector3(7f, 0.5f, 0), new Vector3(-7f, 0.5f, 0) };

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1, 1);
    }

    private void SpawnEnemy()
    {
        if (GameManager.GetInstance().gameState == GameManager.GameState.Playing && EnemyController.enemyCount < 5)
        {
            int randomIndex = Random.Range(0, spawnPositions.Length);
            Vector3 spawnPos = spawnPositions[randomIndex] + new Vector3(0, EnemyController.enemyCount, 0);

            Instantiate(enemy, spawnPos, Quaternion.identity);
        }
    }
}
