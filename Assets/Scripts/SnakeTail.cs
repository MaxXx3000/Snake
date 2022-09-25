using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{
    public Transform SnakeHead;
    public float CircleDiameter;

    public SnakeMovement SnakeMovement;
    public ObstacleObject ObstacleObject;

    private List<Transform> snakeCircles = new List<Transform>();
    private List<Vector3> positions = new List<Vector3>();

    private void Awake()
    {
        positions.Add(SnakeHead.position);
    }

    private void Update()
    {
        float distance = ((Vector3) SnakeHead.position - positions[0]).magnitude;

        if (distance > CircleDiameter)
        {
            // Направление от старого положения головы, к новому
            Vector3 direction = ((Vector3) SnakeHead.position - positions[0]).normalized;

            positions.Insert(0, positions[0] + direction * CircleDiameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= CircleDiameter;
        }

        for (int i = 0; i < snakeCircles.Count; i++)
        {
            snakeCircles[i].position = Vector3.Lerp(positions[i + 1], positions[i], distance / CircleDiameter);
        }
    }
    public void AddStartCircle()
    {
        Transform circle = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeCircles.Add(circle);
        positions.Add(circle.position);
    }
    public void AddCircle()
    {
        for (int i = 0; i < SnakeMovement.grow; i++)
        {
            Transform circle = Instantiate(SnakeHead, positions[positions.Count - 1], Quaternion.identity, transform);
            snakeCircles.Add(circle);
            positions.Add(circle.position);
        }
            
    }

    public void RemoveCircle()
    {
        for (int i = SnakeMovement.damage; i > 0; i--)
        {
            Destroy(snakeCircles[0].gameObject);
            snakeCircles.RemoveAt(0);
            positions.RemoveAt(1);
        }
                    
    }
}
