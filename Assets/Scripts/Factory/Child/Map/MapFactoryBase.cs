using Utils;

public abstract class MapFactoryBase : FactoryBase
{
    protected EMapPoolType _mapType;
    protected MapAssetManager _mapAssetManager;

    protected abstract void Init();

    void Start()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _mapAssetManager = GenericSingleton<MapAssetManager>.Instance;
        Init();
    }
}