using UnityEngine;

public class GuillotineFactory : ThirdWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EThirdWorldObstacleType.Guillotine;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject guillotine = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return guillotine;
    }
}