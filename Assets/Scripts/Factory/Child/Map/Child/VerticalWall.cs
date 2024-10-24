using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWall : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.VerticalWall;
        _factoryManager.EnemyFactory.AddFactory(_mapType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject wall = _objectPoolManager.Push(_mapType, _prefab);
        return wall;
    }
}