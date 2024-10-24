using Utils;

public abstract class FirstWorldEnemyFactoryBase : FactoryBase
{
    protected EFirstWorldEnemyType _enemyType;
    protected EnemyManager _enemyManager;
    protected EnemyObjectPool _enemyObjectPool;

    protected abstract void Init();

    void Start()
    {
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _enemyManager = GenericSingleton<EnemyManager>.Instance;
        _enemyObjectPool = _objectPoolManager.EnemyObjectPool;
        Init();
        _prefab = _enemyManager.Enemys[(int)_enemyType];
    }
}