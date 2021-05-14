using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentGameState { get; private set; }
    public Level CurrentLevel { get; private set; }
    public int TotalScore { get; private set; }

    private List<Level> levels;
    private int score;

    public static event Action<int> ScoreChange;
    public static event Action<GameState> GameStateChange;

    private const string PLAYER_PREFS_CURRENT_LEVEL_INDEX = "CurrentLevel";
    private const string PLAYER_PREFS_TOTAL_SCORE = "TotalScore";
    private const string PLAYER_PREFS_TAP_TO_PLAY = "TapToPlay";

    protected override void Awake()
    {
        base.Awake();

        levels = new List<Level>()
        {
            new Level("Level 1", 1),
            new Level("Level 2", 2),
            new Level("Level 3", 3)
            
            //new Level("Level 1",7),
            //new Level("Level 2",11),
            //new Level("Level 3",15),
            //new Level("Level 4",19),
            //new Level("Level 5",23),
            //new Level("Level 6",27),
            //new Level("Level 7",31)
        };

        string tapToPlay = PlayerPrefs.GetString(PLAYER_PREFS_TAP_TO_PLAY, "t");

        switch (tapToPlay)
        {
            case "t":
                UpdateState(GameState.PREGAME);
                PlayerPrefs.SetString(PLAYER_PREFS_TAP_TO_PLAY, "f");
                break;
            case "f":
                UpdateState(GameState.RUNNING);
                break;
            default:
                break;
        }

        score = 0;
        TotalScore = PlayerPrefs.GetInt(PLAYER_PREFS_TOTAL_SCORE, 0);

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
        TotalScore += score;
        PlayerPrefs.SetInt(PLAYER_PREFS_TOTAL_SCORE, TotalScore); // add current score to total score

        // Check whether it is the last level.
        if (levels.IndexOf(CurrentLevel) == levels.Count - 1)
        {
            UpdateState(GameState.END);
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

    public void StartGame()
    {
        UpdateState(GameState.RUNNING);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        //PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        // Sets tap to play to true before quit game
        PlayerPrefs.SetString(PLAYER_PREFS_TAP_TO_PLAY, "t");
    }

    //private void HandleSceneLoading(Scene scene, LoadSceneMode mode)
    //{
    //}
}