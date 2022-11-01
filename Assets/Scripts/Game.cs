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
    public Achievements Achievements;
    public GameObject winObject;
    public GameObject statObject;
    public GameObject looseObject;
    public GameObject scorePanelObject;

    public int SNKAch;

    private void Start()
    {
        SNKMoney += SNKAch;
        statObject.gameObject.SetActive(true);
        Debug.Log("Money= " + SNKMoney);
        Debug.Log("MaxSnakeLenght= " + MaxSnakeLenght);
    }
    private void Update()
    {
        if (MaxSnakeLenght < SnakeMovement.Length)
        {
            MaxSnakeLenght = SnakeMovement.Length;
        }
    }
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
        Die++;
        SnakeMovement.CurrentScore = 0;
        LevelIndex = 1;
        Score = 0;        
        SnakeLenght = 1;
        Invoke("ReloadLevel", 0);

    }
    public void OnPlayerReachedFinish()
    {
        SNKMoney += 1;

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
        Destroy(scorePanelObject);
        winObject.gameObject.SetActive(true);
        statObject.gameObject.SetActive(true);
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
    public int MaxSnakeLenght
    {
        get => PlayerPrefs.GetInt(MaxSnakeLenghtKey, 0);
        private set
        {
            PlayerPrefs.SetInt(MaxSnakeLenghtKey, value);
            PlayerPrefs.Save();
        }
    }
    public const string MaxSnakeLenghtKey = "MaxSnakeLenght";
    public int Die
    {
        get => PlayerPrefs.GetInt(DieKey, 0);
        private set
        {
            PlayerPrefs.SetInt(DieKey, value);
            PlayerPrefs.Save();
        }
    }
    public const string DieKey = "Die";
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
    public int SNKMoney
    {
        get => PlayerPrefs.GetInt(SNKMoneyKey, 0);
        private set
        {
            PlayerPrefs.SetInt(SNKMoneyKey, value);
            PlayerPrefs.Save();
        }
    }
    public const string SNKMoneyKey = "SNKMoney";
    public void ReloadLevel()
    {
        if (MaxLevel < LevelIndex)
        {
            MaxLevel = LevelIndex;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}