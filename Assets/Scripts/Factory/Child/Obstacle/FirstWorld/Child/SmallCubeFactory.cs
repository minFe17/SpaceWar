using UnityEngine;

public class SmallCubeFactory : FirstWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = EFirstWorldObstacleType.Cube_Small;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }
    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject cube = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return cube;
    }
}