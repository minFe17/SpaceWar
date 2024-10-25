using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFactory : SecondWorldObstacleFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _obstacleType = ESecondWorldObstacleType.Bone ;
        _factoryManager.ObstacleFactory.AddFactory(_obstacleType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject bone = _obstacletPool.ObstaclePool.Push(_obstacleType, _prefab);
        return bone;
    }
}