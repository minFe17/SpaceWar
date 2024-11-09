using UnityEngine;

public class HorizontalWallFactory : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.HorizontalWall;
        _factoryManager.MapFactory.AddFactory(_mapType, this);
        _prefab = _mapAssetManager.HorizontalWall;
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject wall = _objectPoolManager.Push(_mapType, _prefab);
        return wall;
    }
}