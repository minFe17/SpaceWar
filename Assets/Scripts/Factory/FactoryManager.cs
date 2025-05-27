using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class FactoryManager : MonoBehaviour
{
    //╫л╠шео
    Dictionary<Type, IFactorys> _factorys = new Dictionary<Type, IFactorys>();
    EnemyFactory _enemyFactory = new EnemyFactory();
    MapFactory _mapFactory = new MapFactory();
    ObstacleFactory _obstacleFactory = new ObstacleFactory();

    GameObject _factoryPrefab;
    AddressableManager _addressableManager;
    WorldFactory _worldFactory;

    public EnemyFactory EnemyFactory { get => _enemyFactory; }
    public ObstacleFactory ObstacleFactory { get => _obstacleFactory; }
    public MapFactory MapFactory { get => _mapFactory; }
    public WorldFactory WorldFactory { get => _worldFactory; }

    void Awake()
    {
        _factorys.Add(typeof(EBulletPoolType), new Factory<GameObject>());
        _factorys.Add(typeof(ECoinType), new Factory<GameObject>());
        _factorys.Add(typeof(EEventRoomType), new Factory<GameObject>());
        _factorys.Add(typeof(EGroundWorkType), new Factory<GameObject>());
        _factorys.Add(typeof(ECameraType), new Factory<GameObject>());
        _factorys.Add(typeof(EPlayerType), new Factory<GameObject>());
    }

    public async Task Init()
    {
        await LoadAsset();
        CreateFactory();
    }

    async Task LoadAsset()
    {
        if (_factoryPrefab == null)
        {
            if (_addressableManager == null)
                _addressableManager = GenericSingleton<AddressableManager>.Instance;
            _factoryPrefab = await _addressableManager.GetAddressableAsset<GameObject>("Factory");
        }
    }

    void CreateFactory()
    {
        GameObject temp = Instantiate(_factoryPrefab, transform);
        _worldFactory = temp.GetComponent<WorldFactory>();
        _worldFactory.Init();
    }

    public void ChangeWorld()
    {
        _enemyFactory.RemoveFactory();
        _obstacleFactory.RemoveFactory();
        _mapFactory.RemoveFactory();
        _worldFactory.ChangeWorldFactory();
    }

    public void AddFactorys<TEnum, T>(TEnum key, IFactory<T> value) where TEnum : Enum
    {
        IFactorys factory;
        _factorys.TryGetValue(typeof(TEnum), out factory);
        factory.AddFactorys(key, value);
    }

    public T MakeObject<TEnum, T>(TEnum key) where TEnum : Enum
    {
        IFactorys factory;
        _factorys.TryGetValue(typeof(TEnum), out factory);
        return (T)factory.MakeObject(key);
    }
}