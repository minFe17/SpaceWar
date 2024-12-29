using Utils;

public abstract class ThirdWorldObstacleFactoryBase : FactoryBase
{
    protected EThirdWorldObstacleType _obstacleType;
    protected ObstacleAssetManager _obstacleAssetManager;
    protected ObstacleObjectPool _obstacletPool;

    protected abstract void Init();

    void Awake()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _obstacleAssetManager = GenericSingleton<ObstacleAssetManager>.Instance;
        _obstacletPool = _objectPoolManager.ObstacleObjectPool;
    }

    void OnEnable()
    {
        Init();
        _prefab = _obstacleAssetManager.Obstacles[(int)_obstacleType];
    }
}