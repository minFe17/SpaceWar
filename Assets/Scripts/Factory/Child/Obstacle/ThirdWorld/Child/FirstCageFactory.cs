using UnityEngine;

public class FirstCageFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Cage;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject cage = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return cage;
    }
}