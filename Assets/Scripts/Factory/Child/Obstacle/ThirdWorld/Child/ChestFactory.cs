using UnityEngine;

public class ChestFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Chest;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject chest = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return chest;
    }
}