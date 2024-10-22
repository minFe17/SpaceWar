using UnityEngine;
using Utils;

public class PlayerFactory : FactoryBase, IFactory<Player>
{
    EPlayerPoolType _poolType;
    PlayerAssetManager _assetManager;

    void Start()
    {
        _poolType = EPlayerPoolType.Player;
        _factoryManager = GenericSingleton<FactoryManager>.Instance;
        _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _assetManager = GenericSingleton<PlayerAssetManager>.Instance;
        _prefab = _assetManager.Player;
        Init();
    }

    void Init()
    {
        _factoryManager.AddFactorys(_poolType, this);
    }

    Player IFactory<Player>.MakeObject()
    {
        GameObject temp = _objectPoolManager.Push(_poolType, _prefab);
        Player player = temp.GetComponent<Player>();
        return player;
    }
}