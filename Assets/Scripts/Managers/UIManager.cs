using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject menuScreen;

    [SerializeField]
    private TextMeshProUGUI menuTitle;
    [SerializeField]
    private Button successfulButton;
    [SerializeField]
    private Button failedButton;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private VictoryScreen victoryScreen;

    private void OnEnable()
    {
        GameManager.GameStateChange += HandleGameStateChange;
        GameManager.ScoreChange += HandleScoreChange;
    }

    private void OnDisable()
    {
        GameManager.GameStateChange -= HandleGameStateChange;
        GameManager.ScoreChange -= HandleScoreChange;
    }

    private void HandleGameStateChange(GameState state)
    {
        menuScreen.SetActive(state == GameState.FAILED || state == GameState.SUCCESSFUL);

        switch (state)
        {
            case GameState.PREGAME:
                break;
            case GameState.RUNNING:
                break;
            case GameState.SUCCESSFUL:
                SuccessMenu();
                break;
            case GameState.FAILED:
                FailMenu();
                break;
            case GameState.END:
                EndScreen();
                break;
            default:
                break;
        }
    }

    private void HandleScoreChange(int score)
    {
        scoreText.text = "Score: " + score;
    }

    private void SuccessMenu()
    {
        menuTitle.text = "Successful";
        successfulButton.gameObject.SetActive(true);
        failedButton.gameObject.SetActive(false);
    }

    private void FailMenu()
    {
        menuTitle.text = "Game Over";
        successfulButton.gameObject.SetActive(false);
        failedButton.gameObject.SetActive(true);
    }

    private void EndScreen()
    {
        victoryScreen.gameObject.SetActive(true);
    }

    // Button click
    public void Restart()
    {
        GameManager.Instance.RestartLevel();
    }
}
