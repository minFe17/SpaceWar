using UnityEngine;

public class HorizontalDoorFactory : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.HorizontalDoor;
        _factoryManager.MapFactory.AddFactory(_mapType, this);
        _prefab = _mapAssetManager.HorizontalDoor;
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject door = _objectPoolManager.Push(_mapType, _prefab);
        return door;
    }
}