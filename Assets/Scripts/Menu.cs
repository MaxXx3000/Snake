using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{    
    public Scene Main;
    public Game Game;

    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync(0);
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
        SceneManager.LoadScene(1);
    }
}
