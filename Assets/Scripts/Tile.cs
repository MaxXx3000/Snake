using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    public ObstacleType obstacleType;
    bool isStatedTitle = true;
    //Чем именно занята плитка?
    public enum ObstacleType
    {
        None,
        Resource
    }

    public void SetOccupied(ObstacleType t)
    {
        obstacleType = t;
    }
    #region Methods 
    public void CleanTitle()
    {
        obstacleType = ObstacleType.None;
    }
    public void StarteTileValue(bool value)
    {
        isStatedTitle = value;
    }
    #endregion

    #region Booleans
    public bool IsOccupied
    {
        get
        {
            return obstacleType != ObstacleType.None;
        }
    }
    public bool CanSpawnObstacle
    {
        get
        {
            return !isStatedTitle;
        }
    }
    #endregion
}
