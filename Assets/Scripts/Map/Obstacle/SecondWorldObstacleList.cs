using System.Collections.Generic;
using UnityEngine;

public class SecondWorldObstacleList : ObstacleListBase
{
    public override List<GameObject> AddObstacleList()
    {
        for (int i = 0; i < (int)EFirstWorldObstacleType.Max; i++)
        {
            GameObject temp = Resources.Load($"Prefabs/Map/SecondWorld/Obstacle/{(ESecondWorldObstacleType)i}") as GameObject;
            _obstacles.Add(temp);
        }
        return _obstacles;
    }
}
