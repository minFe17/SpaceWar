using UnityEngine;

public class SecondAltarFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Altar_02;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject altar = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return altar;
    }
}