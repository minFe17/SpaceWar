using UnityEngine;

public class ProjectorFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Projector;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject projector = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return projector;
    }
}