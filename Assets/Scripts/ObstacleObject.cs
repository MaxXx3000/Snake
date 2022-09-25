using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class ObstacleObject : MonoBehaviour
{
    public ObststacleType obstacleType;
    public TextMeshPro _text;
    public int HP;

    public enum ObststacleType
    {
        Food,
        Block,
        Finish
    }

    private void Awake()
    {
        _text.text = HP.ToString();
        //GetComponent<Renderer>().material.SetFloat("FloatHP", (HP / 10));
    }
}
