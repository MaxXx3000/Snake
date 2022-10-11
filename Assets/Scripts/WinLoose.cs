using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoose : MonoBehaviour
{
    public Game Game;
    public Player Player;

    public void WinButton()
    {
        FindObjectOfType<Game>().ReloadLevel();
    }
    public void LooseButton()
    {
        FindObjectOfType<Player>().Die();
    }
}
