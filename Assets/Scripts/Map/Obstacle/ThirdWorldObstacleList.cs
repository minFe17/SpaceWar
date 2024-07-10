using System.Collections.Generic;
using UnityEngine;

public class ThirdWorldObstacleList : ObstacleListBase
{
    public override List<GameObject> AddObstacleList()
    {
        for (int i = 0; i < (int)EThirdWorldObstacleType.Max; i++)
        {
            GameObject temp = Resources.Load($"Prefabs/Map/ThirdWorld/Obstacle/{(EThirdWorldObstacleType)i}") as GameObject;
            _obstacles.Add(temp);
        }
        return _obstacles;
    }
}