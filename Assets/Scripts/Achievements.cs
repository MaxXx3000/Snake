using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public Game Game;
    public GameObject achScore;
    public GameObject achScore0;
    public GameObject achScore1;
    public GameObject achScore2;
    public GameObject achScore3;
    public Text TextAchScore;
    private int Score1 = 100;
    private int Score2 = 1000;
    private int Score3 = 10000;
    public int maxLenght;
    public GameObject achSnakeLenght;
    public GameObject achSnakeLenght0;
    public GameObject achSnakeLenght1;
    public GameObject achSnakeLenght2;
    public GameObject achSnakeLenght3;
    public Text TextAchSnakeLenght;
    private int SnakeLenght1 = 50;
    private int SnakeLenght2 = 100;
    private int SnakeLenght3 = 500;

    void Update()
    {
        if (maxSnakeLenght < maxLenght)
        {
            maxSnakeLenght = maxLenght;
        }

        AchievementScore();
        AchievementSnakeLenght();
    }

    public void AchievementScore()
    {
        if (Game.BestScore < Score1)
        {
            achScore = achScore0;
            achScore.SetActive(true);
            TextAchScore.text = Score1.ToString();
        }
        else if (Game.BestScore >= Score1)
        {
            achScore = achScore1;
            achScore.SetActive(true);
            TextAchScore.text = Score2.ToString();
        }
        else if (Game.BestScore >= Score2)
        {
            achScore = achScore2;
            achScore.SetActive(true);
            TextAchScore.text = Score3.ToString();
        }
        else if (Game.BestScore >= Score3)
        {
            achScore = achScore3;
            achScore.SetActive(true);
            TextAchScore.text = "XXXX".ToString();
        }
    }
    public void AchievementSnakeLenght()
    {
        if (maxSnakeLenght < SnakeLenght1)
        {
            achSnakeLenght = achSnakeLenght0;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = SnakeLenght1.ToString();
        }
        else if (maxSnakeLenght >= SnakeLenght1)
        {
            achSnakeLenght = achSnakeLenght1;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = SnakeLenght2.ToString();
        }
        else if (maxSnakeLenght >= SnakeLenght2)
        {
            achSnakeLenght = achSnakeLenght2;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = SnakeLenght3.ToString();
        }
        else if (maxSnakeLenght >= SnakeLenght3)
        {
            achSnakeLenght = achSnakeLenght3;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = "XXXX".ToString();
        }
    }

    public int maxSnakeLenght
    {
        get => PlayerPrefs.GetInt(maxSnakeLenghtKey, 0);
        private set
        {
            PlayerPrefs.SetInt(maxSnakeLenghtKey, value);
            PlayerPrefs.Save();
        }
    }

    public const string maxSnakeLenghtKey = "maxSnakeLenght";
}


