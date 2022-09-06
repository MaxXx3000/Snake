using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public ObststacleType obstacleType;
    public int resourceAmount;

    public enum ObststacleType
    {
        Food,
        Block,
        Finish
    }
}
