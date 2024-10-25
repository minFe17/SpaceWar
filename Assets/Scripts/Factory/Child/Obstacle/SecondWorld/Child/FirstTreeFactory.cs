using UnityEngine;

public class FirstTreeFactory : SecondWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = ESecondWorldObstacleType.Tree;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject tree = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return tree;
    }
}