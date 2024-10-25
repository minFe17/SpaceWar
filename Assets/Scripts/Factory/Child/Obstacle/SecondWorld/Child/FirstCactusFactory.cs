using UnityEngine;

public class FirstCactusFactory : SecondWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = ESecondWorldObstacleType.Cactus;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject cactus = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return cactus;
    }
}