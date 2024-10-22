using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class FactoryManager : MonoBehaviour
{
    //╫л╠шео
    Dictionary<Type, IFactorys> _factorys = new Dictionary<Type, IFactorys>();
    GameObject _factoryPrefab;
    AddressableManager _addressableManager;

    void Awake()
    {
        _factorys.Add(typeof(EPlayerPoolType), new Factory<EPlayerPoolType>());
        _factorys.Add(typeof(ECoinType), new Factory<ECoinType>());
        _factorys.Add(typeof(EEventRoomType), new Factory<EEventRoomType>());
        _factorys.Add(typeof(EGroundWorkType), new Factory<EGroundWorkType>());
        _factorys.Add(typeof(ECameraType), new Factory<ECameraType>());
        _factorys.Add(typeof(EMapPoolType), new Factory<EMapPoolType>());
    }

    public async void Init()
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
        Instantiate(_factoryPrefab, transform);
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