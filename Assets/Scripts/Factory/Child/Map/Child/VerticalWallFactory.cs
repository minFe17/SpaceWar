using UnityEngine;

public class VerticalWallFactory : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.VerticalWall;
        _factoryManager.MapFactory.AddFactory(_mapType, this);
        _prefab = _mapAssetManager.VerticalWall;
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject wall = _objectPoolManager.Push(_mapType, _prefab);
        return wall;
    }
}