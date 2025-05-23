using UnityEngine;
using Utils;

public class SoldierFactory : FactoryBase, IFactory<GameObject>
{
    EPlayerPoolType _poolType;
    PlayerAssetManager _assetManager;

    void Start()
    {
        _poolType = EPlayerPoolType.Player;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _assetManager = GenericSingleton<PlayerAssetManager>.Instance;
        _prefab = _assetManager.Soldier;
        Init();
    }

    void Init()
    {
        _factoryManager.AddFactorys(_poolType, this);
    }

    GameObject IFactory<GameObject>.MakeObject()
    {
        GameObject player = _objectPoolManager.Push(_poolType, _prefab);
        return player;
    }
}