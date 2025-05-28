using UnityEngine;
using Utils;

public class ThunderBallFactory : FactoryBase, IFactory<GameObject>
{
    EBulletPoolType _poolType;
    BulletAssetManager _assetManager;

    void Start()
    {
        _poolType = EBulletPoolType.ThunderBall;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _assetManager = GenericSingleton<BulletAssetManager>.Instance;
        _prefab = _assetManager.ThunderBall;
        Init();
    }

    void Init()
    {
        _factoryManager.AddFactorys(_poolType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject bullet = _objectPoolManager.Push(_poolType, _prefab);
        return bullet;
    }
}