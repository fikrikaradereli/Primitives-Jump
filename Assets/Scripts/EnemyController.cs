using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    private enum EnemyType { Left, Right }
    private EnemyType enemyType;

    public float speed;
    private readonly float heightDifference = 0.95f;
    private readonly float startingPos = 6f;
    private readonly float force = 10f;


    public static event Action PlayerDeath;
    public static event Action<int> ScoreAdd;

    void Awake()
    {
        if (transform.position.x > startingPos)
        {
            enemyType = EnemyType.Right;
        }

        if (transform.position.x < -startingPos)
        {
            enemyType = EnemyType.Left;
        }
    }

    void Update()
    {
        if (enemyType == EnemyType.Right)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (enemyType == EnemyType.Left)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((collision.gameObject.transform.position.y - transform.position.y) > heightDifference)
            {
                // Enemy durmu�sa yeniden skor eklemeyi �nler.
                if (speed != 0)
                {
                    speed = 0;
                    AudioManager.Instance.PlayJumpSound();

                    if (ScoreAdd != null)
                    {
                        ScoreAdd(5);
                    }
                }
            }
            else
            {
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromEnemy = collision.gameObject.transform.position - transform.position;
                playerRb.AddForce(awayFromEnemy * force, ForceMode.Impulse);

                if (PlayerDeath != null)
                {
                    PlayerDeath();
                }
            }
        }
    }
}
