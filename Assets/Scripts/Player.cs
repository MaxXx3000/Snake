using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BounceSpeed;
    public Rigidbody Rigidbody;
    public ParticleSystem effectDie;
    public ParticleSystem effectFinish;

    public SnakeMovement SnakeMovement;
    public Game Game;

    public void ReachFinish()
    {
        //Invoke("Game.OnPlayerReachedFinish", 1);
        Game.OnPlayerReachedFinish();
        Rigidbody.velocity = Vector3.zero;
        //effectFinish.Play();
        
    }

    public void Die()
    {
        //Invoke("Game.OnPlayerDied", 1);
        Game.OnPlayerDied();
        Rigidbody.velocity = Vector3.zero;
        //effectDie.Play();
    }
}