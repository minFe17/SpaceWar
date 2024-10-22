using Utils;

public abstract class GroundWorkFactoryBase : FactoryBase
{
    protected EGroundWorkType _groundWorkType;
    protected MapAssetManager _mapAssetMaanger;

    protected abstract void Init();

    void Start()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _mapAssetMaanger = GenericSingleton<MapAssetManager>.Instance;
        Init();
    }
}