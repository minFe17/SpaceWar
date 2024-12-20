using System;
using System.Threading.Tasks;
using UnityEngine;

public class ThirdWorld : IEnemyList
{
    EnemyManager _enemyManager;
    AddressableManager _addressableManager;

    async Task IEnemyList.AddEnemyList(EnemyManager enemyManager, AddressableManager addressableManager)
    {
        _enemyManager = enemyManager;
        _addressableManager = addressableManager;

        if (_enemyManager.Enemys.Count != 0)
            return;

        await LoadEnemy();
        await LoadRock();
    }

    Enum IEnemyList.ConvertEnumToInt(int value)
    {
        return (EThirdWorldEnemyType)value;
    }

    void IEnemyList.ReleaseAsset()
    {
        if (_enemyManager.Rock != null)
            _addressableManager.Release(_enemyManager.Rock);
    }

    void IEnemyList.MakePool(EnemyObjectPool enemyObjectPool)
    {
        enemyObjectPool.CreatePool<EThirdWorldEnemyType>();
    }

    async Task LoadEnemy()
    {
        for (int i = 0; i < (int)EThirdWorldEnemyType.Max; i++)
        {
            GameObject temp = await _addressableManager.GetAddressableAsset<GameObject>($"ThirdWorld/{(EThirdWorldEnemyType)i}.prefab");
            _enemyManager.Enemys.Add(temp);
        }
        _enemyManager.Boss = await _addressableManager.GetAddressableAsset<GameObject>("ThirdWorld/Dragon.prefab");
    }

    async Task LoadRock()
    {
        _enemyManager.Missile = await _addressableManager.GetAddressableAsset<GameObject>("ThirdWorld/Rock.prefab");
    }
}