using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }
    public Level CurrentLevel { get; private set; }

    private List<Level> levels;
    private int score;

    public static event Action<int> ScoreChange;
    public static event Action<GameState> GameStateChange;

    private const string PLAYER_PREFS_CURRENT_LEVEL_INDEX = "CurrentLevel";

    protected override void Awake()
    {
        base.Awake();

        levels = new List<Level>()
        {
            new Level("Level 1", 1),
            new Level("Level 2", 2),
            new Level("Level 3", 3)
        };

        UpdateState(GameState.RUNNING);

        score = 0;

        int currentLevelIndex = PlayerPrefs.GetInt(PLAYER_PREFS_CURRENT_LEVEL_INDEX, -1);

        // If PLAYER_PREFS_CURRENT_LEVEL_INDEX does not exist it is return -1
        if (currentLevelIndex == -1)
        {
            CurrentLevel = levels[0];
        }
        else
        {
            CurrentLevel = levels[currentLevelIndex];
        }
    }

    void OnEnable()
    {
        //SceneManager.sceneLoaded += HandleSceneLoading;

        Spawner.LevelSuccessful += LevelPassed;
        EnemyController.PlayerDeath += LevelFailed;
        EnemyController.ScoreAdd += UpdateScore;
    }

    void OnDisable()
    {
        //SceneManager.sceneLoaded -= HandleSceneLoading;

        Spawner.LevelSuccessful -= LevelPassed;
        EnemyController.PlayerDeath -= LevelFailed;
        EnemyController.ScoreAdd -= UpdateScore;
    }

    private void UpdateState(GameState state)
    {
        CurrentGameState = state;
        GameStateChange?.Invoke(state); // Emit the event
    }

    private void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ScoreChange?.Invoke(score); // Event
    }

    private void LevelPassed()
    {
        // Check whether it is the last level.
        if (levels.IndexOf(CurrentLevel) == levels.Count - 1)
        {
            UpdateState(GameState.END);
            Debug.Log("THE END");
        }
        else
        {
            UpdateState(GameState.SUCCESSFUL);
            PlayerPrefs.SetInt(PLAYER_PREFS_CURRENT_LEVEL_INDEX, levels.IndexOf(CurrentLevel) + 1); // increase the level
        }
    }

    private void LevelFailed()
    {
        UpdateState(GameState.FAILED);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    //private void HandleSceneLoading(Scene scene, LoadSceneMode mode)
    //{
    //}
}