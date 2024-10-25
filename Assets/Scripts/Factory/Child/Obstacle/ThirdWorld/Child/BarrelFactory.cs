using UnityEngine;

public class BarrelFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Barrel;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject barrel = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return barrel;
    }
}