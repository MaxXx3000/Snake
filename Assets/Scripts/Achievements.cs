using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public Game Game;
    public SnakeMovement SnakeMovement;

    public GameObject achScore;
    public GameObject achScore0;
    public GameObject achScore1;
    public GameObject achScore2;
    public GameObject achScore3;
    public Text TextAchScore;
    private int Score1 = 100;
    private int Score2 = 1000;
    private int Score3 = 10000;
    public GameObject achSnakeLenght;
    public GameObject achSnakeLenght0;
    public GameObject achSnakeLenght1;
    public GameObject achSnakeLenght2;
    public GameObject achSnakeLenght3;
    public Text TextAchSnakeLenght;
    private int SnakeLenght1 = 50;
    private int SnakeLenght2 = 100;
    private int SnakeLenght3 = 500;
    public GameObject achLevel;
    public GameObject achLevel0;
    public GameObject achLevel1;
    public GameObject achLevel2;
    public GameObject achLevel3;
    public Text TextAchLevel;
    private int Level1 = 10;
    private int Level2 = 50;
    private int Level3 = 100;
    public GameObject achDie;
    public GameObject achDie0;
    public GameObject achDie1;
    public GameObject achDie2;
    public GameObject achDie3;
    public Text TextAchDie;
    private int Die1 = 10;
    private int Die2 = 50;
    private int Die3 = 100;

    private void Start()
    {
        AchievementScore();
        AchievementSnakeLenght();
        AchievementLevel();
        AchievementDie();
    }
  
    public void AchievementScore()
    {
        if (Game.BestScore < Score1)
        {
            achScore = achScore0;
            achScore.SetActive(true);
            TextAchScore.text = Score1.ToString();
        }
        else if (Game.BestScore >= Score1 && Game.BestScore < Score2)
        {
            achScore = achScore1;
            achScore.SetActive(true);
            TextAchScore.text = Score2.ToString();
        }
        else if (Game.BestScore >= Score2 && Game.BestScore < Score3)
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
    public void AchievementDie()
    {
        if (Game.Die < Die1)
        {
            achDie = achDie0;
            achDie.SetActive(true);
            TextAchDie.text = Die1.ToString();
        }
        else if (Game.Die >= Die1 && Game.Die < Die2)
        {
            achDie = achDie1;
            achDie.SetActive(true);
            TextAchDie.text = Die2.ToString();
        }
        else if (Game.Die >= Die2 && Game.Die < Die3)
        {
            achDie = achDie2;
            achDie.SetActive(true);
            TextAchDie.text = Die3.ToString();
        }
        else if (Game.Die >= Die3)
        {
            achDie = achDie3;
            achDie.SetActive(true);
            TextAchDie.text = "XXXX".ToString();
        }
    }
    public void AchievementLevel()
    {
        if (Game.MaxLevel < Level1)
        {
            achLevel = achLevel0;
            achLevel.SetActive(true);
            TextAchLevel.text = Level1.ToString();
        }
        else if (Game.MaxLevel >= Level1 && Game.MaxLevel < Level2)
        {
            achLevel = achLevel1;
            achLevel.SetActive(true);
            TextAchLevel.text = Level2.ToString();
        }
        else if (Game.MaxLevel >= Level2 && Game.MaxLevel < Level3)
        {
            achLevel = achLevel2;
            achLevel.SetActive(true);
            TextAchLevel.text = Level3.ToString();
        }
        else if (Game.MaxLevel >= Level3)
        {
            achLevel = achLevel3;
            achLevel.SetActive(true);
            TextAchLevel.text = "XXXX".ToString();
        }
    }
    public void AchievementSnakeLenght()
    {
        if (Game.MaxSnakeLenght < SnakeLenght1)
        {
            achSnakeLenght = achSnakeLenght0;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = SnakeLenght1.ToString();
        }
        else if (Game.MaxSnakeLenght >= SnakeLenght1 && Game.MaxSnakeLenght < SnakeLenght2)
        {
            achSnakeLenght = achSnakeLenght1;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = SnakeLenght2.ToString();
        }
        else if (Game.MaxSnakeLenght >= SnakeLenght2 && Game.MaxSnakeLenght < SnakeLenght3)
        {
            achSnakeLenght = achSnakeLenght2;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = SnakeLenght3.ToString();
        }
        else if (Game.MaxSnakeLenght >= SnakeLenght3)
        {
            achSnakeLenght = achSnakeLenght3;
            achSnakeLenght.SetActive(true);
            TextAchSnakeLenght.text = "XXXX".ToString();
        }
    }
}


