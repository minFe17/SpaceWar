using Utils;

public abstract class FirstWorldObstacleFactoryBase : FactoryBase
{
    protected EFirstWorldObstacleType _obstacleType;
    protected ObstacleAssetManager _obstacleAssetManager;
    protected ObstacleObjectPool _obstacletPool;

    protected abstract void Init();

    void Start()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _obstacleAssetManager = GenericSingleton<ObstacleAssetManager>.Instance;
        _obstacletPool = _objectPoolManager.ObstacleObjectPool;
        Init();
        _prefab = _obstacleAssetManager.Obstacles[(int)_obstacleType];
    }
}