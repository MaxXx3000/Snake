using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Game Game;
    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<Game>().OnPlayerDied();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
