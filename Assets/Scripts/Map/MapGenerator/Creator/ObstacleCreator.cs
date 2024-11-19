using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class ObstacleCreator : MonoBehaviour
{
    List<IObstacle> _obstacles = new List<IObstacle>();

    ObstacleAssetManager _obstacleAssetManager;
    FactoryManager _factoryManager;

    int _minObstacle;
    int _maxObstacle;

    public void Init(int minObstacle, int maxObstacle)
    {
        _minObstacle = minObstacle;
        _maxObstacle = maxObstacle;
        _obstacleAssetManager = GenericSingleton<ObstacleAssetManager>.Instance;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
    }

    public void CreateObstacle(Vector2 bottomLeftCorner, Vector2 topRightCorner, Transform parent)
    {
        List<GameObject> obstacles = GenericSingleton<ObstacleAssetManager>.Instance.Obstacles;
        int obstacleCount = Random.Range(_minObstacle, _maxObstacle);
        for (int i = 0; i < obstacleCount; i++)
        {
            int random = Random.Range(0, obstacles.Count);
            Vector3 position = new Vector3(Random.Range(bottomLeftCorner.x + 10, topRightCorner.x - 10), 0, Random.Range(bottomLeftCorner.y + 10, topRightCorner.y - 10));
            Enum type = _obstacleAssetManager.ConvertEnumToInt(random);
            GameObject obstacle = _factoryManager.ObstacleFactory.MakeObject(type);
            obstacle.transform.position = position;
            obstacle.transform.parent = parent;
            _obstacles.Add(obstacle.GetComponent<IObstacle>());
        }
    }

    public void DestroyObstacle()
    {
        for (int i = 0; i < _obstacles.Count; i++)
            _obstacles[i].DestroyObstacle();
        _obstacles.Clear();
    }
}