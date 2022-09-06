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
        Game.OnPlayerReachedFinish();
        Rigidbody.velocity = Vector3.zero;
        //effectFinish.Play();
        
    }

    public void Die()
    {
        Game.OnPlayerDied();
        Rigidbody.velocity = Vector3.zero;
        //effectDie.Play();
    }
}