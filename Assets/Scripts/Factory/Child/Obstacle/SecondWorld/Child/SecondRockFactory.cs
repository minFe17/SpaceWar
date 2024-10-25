using UnityEngine;

public class SecondRockFactory : SecondWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = ESecondWorldObstacleType.Rock_02;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject rock = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return rock;
    }
}