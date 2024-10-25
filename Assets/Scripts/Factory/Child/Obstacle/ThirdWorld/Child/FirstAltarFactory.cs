using UnityEngine;

public class FirstAltarFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Altar;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject altar = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return altar;
    }
}