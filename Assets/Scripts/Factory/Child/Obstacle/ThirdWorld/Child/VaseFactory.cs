using UnityEngine;

public class VaseFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Vase;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject vase = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return vase;
    }
}