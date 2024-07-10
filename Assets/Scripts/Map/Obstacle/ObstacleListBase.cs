using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleListBase : MonoBehaviour
{
    protected List<GameObject> _obstacles = new List<GameObject>();
    public abstract List<GameObject> AddObstacleList();
}