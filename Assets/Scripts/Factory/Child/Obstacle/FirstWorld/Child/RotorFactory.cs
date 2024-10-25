using UnityEngine;

public class RotorFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Rotor;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }
    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject rotor = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return rotor;
    }
}