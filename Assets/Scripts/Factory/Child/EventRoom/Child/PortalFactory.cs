using UnityEngine;

public class PortalFactory : EventRoomFactoryBase, IFactory<GameObject>
{
    protected override void Init()
    {
        _eventRoomType = EEventRoomType.Portal;
        _factoryManager.AddFactorys(_eventRoomType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject portal = _objectPoolManager.Push(_eventRoomType, _prefab);
        portal.GetComponent<EventRoom>().Init();
        return portal;
    }
}