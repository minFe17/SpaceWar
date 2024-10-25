using UnityEngine;

public class GeneratorFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Generator;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject generator = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return generator;
    }
}