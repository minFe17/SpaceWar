using UnityEngine;

public class VerticalDoorFactory : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.VerticalDoor;
        _factoryManager.MapFactory.AddFactory(_mapType, this);
        _prefab = _mapAssetManager.VerticalDoor;
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject door = _objectPoolManager.Push(_mapType, _prefab);
        return door;
    }
}