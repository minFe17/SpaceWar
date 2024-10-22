using Utils;

public abstract class EventRoomFactoryBase : FactoryBase
{
    protected EEventRoomType _eventRoomType;
    protected MapAssetManager _mapAssetMaanger;

    protected abstract void Init();

    void Start()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _mapAssetMaanger = GenericSingleton<MapAssetManager>.Instance;
        Init();
        _mapAssetMaanger.EventRooms.TryGetValue(_eventRoomType, out _prefab);
    }
}