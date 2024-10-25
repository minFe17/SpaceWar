using UnityEngine;

public class BigCubeFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Cube_Big;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }
    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject cube = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return cube;
    }
}