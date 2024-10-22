using UnityEngine;
using Utils;

public class PlayerBulletFactory : FactoryBase, IFactory<GameObject>
{
    EPlayerPoolType _poolType;
    PlayerAssetManager _assetManager;

    void Start()
    {
        _poolType = EPlayerPoolType.Bullet;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _assetManager = GenericSingleton<PlayerAssetManager>.Instance;
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