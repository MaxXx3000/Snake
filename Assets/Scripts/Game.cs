using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public SnakeMovement snakeMovement;
    public SnakeMovement SnakeMovement;
    public enum State
    {
        Playing,
        Won,
        Loss,
    }
    public State CurrentState { get; private set; }
    public int Currentcore;
    public int Currentlenght = 2;
    public void OnPlayerDied()
    {
        if (CurrentState != State.Playing) return;

        CurrentState = State.Loss;
        snakeMovement.enabled = false;
        Score = Score + SnakeMovement.CurrentScore;
        if (BestScore < Score)
        {
            BestScore = Score;
        }
        SnakeMovement.CurrentScore = 0;
        LevelIndex = 1;
        Score = 0;        
        SnakeLenght = 1;
        Debug.Log("Game over");
        Invoke("ReloadLevel", 2);
    }
    
    public void OnPlayerReachedFinish()
    {
        if (CurrentState != State.Playing) return;

        Currentcore = SnakeMovement.CurrentScore;
        SnakeLenght = SnakeMovement.Length;
        CurrentState = State.Won;
        snakeMovement.enabled = false;
        Score = Score + SnakeMovement.CurrentScore;
        if (BestScore < Score)
        {
            BestScore = Score;
        }
        LevelIndex++;
        Debug.Log("You won");
        Invoke("ReloadLevel", 2);
    }
    public int SnakeLenght
    {
        get => PlayerPrefs.GetInt(SnakeLenghtKey, 0);
        private set
        {
            PlayerPrefs.SetInt(SnakeLenghtKey, value);
            PlayerPrefs.Save();
        }
    }
    public const string SnakeLenghtKey = "SnakeLenght";
    public int LevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 1);
        private set
        {
            PlayerPrefs.SetInt(LevelIndexKey, value);
            PlayerPrefs.Save();
        }
    }

    public const string LevelIndexKey = "LevelIndex";

    public int BestScore
    {
        get => PlayerPrefs.GetInt(BestScoreKey, 0);
        private set
        {
            PlayerPrefs.SetInt(BestScoreKey, value);
            PlayerPrefs.Save();
        }
    }

    public const string BestScoreKey = "BestScore";

    public int Score
    {
        get => PlayerPrefs.GetInt(ScoreKey, 0);
        private set
        {
            PlayerPrefs.SetInt(ScoreKey, value);
            PlayerPrefs.Save();
        }
    }

    public const string ScoreKey = "Score";

    public int MaxLevel
    {
        get => PlayerPrefs.GetInt(MaxLevelKey, 0);
        private set
        {
            PlayerPrefs.SetInt(MaxLevelKey, value);
            PlayerPrefs.Save();
        }
    }

    public const string MaxLevelKey = "MaxLevel";

    public void ReloadLevel()
    {
        if (MaxLevel < LevelIndex)
        {
            MaxLevel = LevelIndex;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}