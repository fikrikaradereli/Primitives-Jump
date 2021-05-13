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
    private TextMeshProUGUI menuScreenTotalScoreText;
    [SerializeField]
    private Button successfulButton;
    [SerializeField]
    private Button failedButton;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI levelText;

    [SerializeField]
    private VictoryScreen victoryScreen;

    [SerializeField]
    private GameObject endingMenu;
    [SerializeField]
    private TextMeshProUGUI endingMenuTotalScoreText;
    [SerializeField]
    private Button quitButton;

    private void Start()
    {
        levelText.text = GameManager.Instance.CurrentLevel.Name;
    }

    private void OnEnable()
    {
        GameManager.GameStateChange += HandleGameStateChange;
        GameManager.ScoreChange += HandleScoreChange;
        VictoryScreen.VictoryEnd += HandleVictoryEnd;
    }

    private void OnDisable()
    {
        GameManager.GameStateChange -= HandleGameStateChange;
        GameManager.ScoreChange -= HandleScoreChange;
        VictoryScreen.VictoryEnd -= HandleVictoryEnd;
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
                VictoryScreenAnim();
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
        menuScreenTotalScoreText.gameObject.SetActive(true);
        menuScreenTotalScoreText.text = "Total Score: " + GameManager.Instance.TotalScore;
        successfulButton.gameObject.SetActive(true);
        failedButton.gameObject.SetActive(false);
    }

    private void FailMenu()
    {
        menuTitle.text = "Game Over";
        menuScreenTotalScoreText.gameObject.SetActive(false);
        successfulButton.gameObject.SetActive(false);
        failedButton.gameObject.SetActive(true);
    }

    private void VictoryScreenAnim()
    {
        victoryScreen.gameObject.SetActive(true);
    }

    private void HandleVictoryEnd()
    {
        endingMenu.transform.localScale = Vector2.zero;
        endingMenuTotalScoreText.text = "Total Score: " + GameManager.Instance.TotalScore;
        endingMenu.SetActive(true);
        endingMenu.LeanScale(Vector2.one, 0.7f).setEaseOutQuad().delay = 0.2f;
    }

    #region Button Clicks

    public void Restart()
    {
        GameManager.Instance.RestartLevel();
    }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

    #endregion
}