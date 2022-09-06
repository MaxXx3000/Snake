using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public SnakeMovement SnakeMovement;
    public GameManager GameManager;
    public Slider slider;
    private float _startZ;
    private float _endZ;
    private float _minimumReachedZ;
    private void Start()
    {
        _startZ = SnakeMovement.transform.position.z;        
    }

    private void Update()
    {
        _minimumReachedZ = SnakeMovement.transform.position.z;
        float t = Mathf.InverseLerp(_startZ, GameManager.finishZ, _minimumReachedZ);
        slider.value = t;
    }
}
