using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
    }
}
