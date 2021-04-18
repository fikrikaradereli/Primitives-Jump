using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, GameOver, Menu }
    public GameState gameState;

    private float score;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private static GameManager instance;
    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        score = 0;
    }

    void OnEnable()
    {
        EnemyController.PlayerDeath += GameOver;
        EnemyController.ScoreAdd += UpdateScore;
    }

    void OnDisable()
    {
        EnemyController.PlayerDeath -= GameOver;
        EnemyController.ScoreAdd -= UpdateScore;

    }

    private void GameOver()
    {
        gameState = GameState.GameOver;
        Debug.Log("Game State in GameOver function: " + gameState);
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}