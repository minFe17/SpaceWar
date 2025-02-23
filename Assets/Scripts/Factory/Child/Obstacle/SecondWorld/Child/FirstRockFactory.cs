using UnityEngine;

public class FirstRockFactory : SecondWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = ESecondWorldObstacleType.Rock;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject rock = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return rock;
    }
}