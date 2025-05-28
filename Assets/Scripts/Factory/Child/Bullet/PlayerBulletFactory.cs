using UnityEngine;
using Utils;

public class PlayerBulletFactory : FactoryBase, IFactory<GameObject>
{
    EBulletPoolType _poolType;
    BulletAssetManager _assetManager;

    void Start()
    {
        _poolType = EBulletPoolType.Bullet;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _assetManager = GenericSingleton<BulletAssetManager>.Instance;
        _prefab = _assetManager.Bullet;
        Init();
    }

    void Init()
    {
        _factoryManager.AddFactorys(_poolType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject bullet = _objectPoolManager.Push(_poolType, _prefab);
        bullet.GetComponent<Bullet>().Init();
        return bullet;
    }
}