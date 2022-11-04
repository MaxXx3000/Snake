using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class Player : MonoBehaviour
{
    public float BounceSpeed;
    public Rigidbody Rigidbody;
    public ParticleSystem effectDie;
    public ParticleSystem effectFinish;

    public YandexMobileAdsInterstitialDemoScript YandexMobileAdsInterstitialDemoScript;
    public SnakeMovement SnakeMovement;
    public Game Game;

    public void ReachFinish()
    {
        YandexMobileAdsInterstitialDemoScript.RequestInterstitial();
        Invoke("Game.OnPlayerReachedFinish", 5);
        //Game.OnPlayerReachedFinish();
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