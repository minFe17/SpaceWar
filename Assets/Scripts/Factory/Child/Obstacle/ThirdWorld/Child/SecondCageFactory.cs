using UnityEngine;

public class SecondCageFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Cage_01;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject cage = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return cage;
    }
}