using UnityEngine;

public class HorizontalWall : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.HorizontalWall;
        _factoryManager.EnemyFactory.AddFactory(_mapType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject wall = _objectPoolManager.Push(_mapType, _prefab);
        return wall;
    }
}