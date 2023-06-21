using System.Collections.Generic;
using UnityEngine;

public class FirstWorldObstacleList : ObstacleListBase
{
    public override List<GameObject> AddObstacleList()
    {
        for (int i = 0; i < (int)EFirstWorldObstacleType.Max; i++)
        {
            GameObject temp = Resources.Load($"Prefabs/Map/FirstWorld/Obstacle/{(EFirstWorldObstacleType)i}") as GameObject;
            _obstacles.Add(temp);
        }
        return _obstacles;
    }
}
