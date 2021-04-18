using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum EnemyType { Left, Right }

    private EnemyType enemyType;
    private float speed = 5f;
    private float heightDifference = 0.9f;
    private float startingPos = 6f;
    private float force = 10f;

    public static int enemyCount = 0;

    public delegate void PlayerDeathDelegate();
    public static event PlayerDeathDelegate PlayerDeath;

    public delegate void ScoreDelegate(int score);
    public static event ScoreDelegate ScoreAdd;

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

        enemyCount++;
    }

    // Update is called once per frame
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
                // Enemy durmuþsa yeniden skor eklemeyi önler.
                if (speed != 0)
                {
                    speed = 0;

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
