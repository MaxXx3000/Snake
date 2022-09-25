using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class ObstacleObject : MonoBehaviour
{
    public ObststacleType obstacleType;
    public SnakeMovement SnakeMovement;
    public TextMeshPro _text;
    public int hp;

    public enum ObststacleType
    {
        Food,
        Block,
        Finish
    }

    private void Awake()
    {
        _text.text = hp.ToString();
        //gameObject.GetComponent<Renderer>().material.SetFloat("FloatHP", (hp / 10));
    }
}
