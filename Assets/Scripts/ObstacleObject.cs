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
    public float floatHP;
    public float hpf;
    public float hpMax = 20f;
    public int tmplMax = 20;

    public enum ObststacleType
    {
        Food,
        Block,
        Finish
    }

    private void Awake()
    {
        _text.text = hp.ToString();
        hpf = hp;
        floatHP = hpf / hpMax;
        GetComponent<Renderer>().material.SetFloat("_FloatHP", floatHP);
    }

    public void ChangeBlock()
    {
        hp--;
        _text.text = hp.ToString();        
        hpf = hp;
        floatHP = hpf / hpMax;
        Debug.Log("floatHP" + floatHP);
        //SnakeMovement.currentBlock.GetComponent<Renderer>().material.SetFloat("_FloatHP", floatHP);
    }
}
