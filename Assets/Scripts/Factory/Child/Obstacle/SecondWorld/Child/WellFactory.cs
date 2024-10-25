using UnityEngine;

public class WellFactory : SecondWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = ESecondWorldObstacleType.Well;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject well = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return well;
    }
}