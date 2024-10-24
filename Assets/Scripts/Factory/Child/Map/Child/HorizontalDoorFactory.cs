using UnityEngine;

public class HorizontalDoorFactory : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.HorizontalDoor;
        _factoryManager.EnemyFactory.AddFactory(_mapType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject door = _objectPoolManager.Push(_mapType, _prefab);
        return door;
    }
}