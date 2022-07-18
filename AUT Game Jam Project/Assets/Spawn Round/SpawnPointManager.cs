using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public List<Transform> points = new List<Transform>();

    public Transform GetRandomPoint()
    {
        return points[Random.Range(0, points.Count)];
    }
    public Vector2 GetRandomPosition()
    {
        return GetRandomPoint().position;
    }
}
