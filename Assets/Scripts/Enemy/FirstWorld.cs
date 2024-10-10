using System.Threading.Tasks;
using UnityEngine;

public class FirstWorld : IWorldEnemyListBase
{
    EnemyManager _enemyManager;
    AddressableManager _addressableManager;

    async Task IWorldEnemyListBase.AddEnemyList(EnemyManager enemyManager, AddressableManager addressableManager)
    {
        _enemyManager = enemyManager;
        _addressableManager = addressableManager;

        if (_enemyManager.Enemys.Count != 0)
            return;

        await LoadEnemy();
        await LoadMissile();
    }

    void IWorldEnemyListBase.ReleaseAsset()
    {
        if (_enemyManager.Missile != null)
            _addressableManager.Release(_enemyManager.Missile);
    }

    async Task LoadEnemy()
    {
        for (int i = 0; i < (int)EFirstWorldEnemyType.Max; i++)
        {
            GameObject temp = await _addressableManager.GetAddressableAsset<GameObject>($"FirstWorld/{(EFirstWorldEnemyType)i}.prefab");
            _enemyManager.Enemys.Add(temp);
        }
        _enemyManager.Boss = await _addressableManager.GetAddressableAsset<GameObject>("FirstWorld/Rhino.prefab");
    }

    async Task LoadMissile()
    {
        _enemyManager.Missile = await _addressableManager.GetAddressableAsset<GameObject>("FirstWorld/Missile.prefab");
    }
}