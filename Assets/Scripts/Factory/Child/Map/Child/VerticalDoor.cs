using UnityEngine;

public class VerticalDoor : MapFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _mapType = EMapPoolType.VerticalDoor;
        _factoryManager.EnemyFactory.AddFactory(_mapType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject door = _objectPoolManager.Push(_mapType, _prefab);
        return door;
    }
}