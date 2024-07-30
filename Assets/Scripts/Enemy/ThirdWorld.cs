using System.Threading.Tasks;
using UnityEngine;

public class ThirdWorld : IWorldEnemyListBase
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
        await LoadRock();
    }

    void IWorldEnemyListBase.ReleaseAsset()
    {
        if (_enemyManager.Rock != null)
            _addressableManager.Release(_enemyManager.Rock);
    }

    async Task LoadEnemy()
    {
        for (int i = 0; i < (int)EThirdWorldEnemyType.Max; i++)
        {
            GameObject temp = await _addressableManager.GetAddressableAsset<GameObject>($"ThirdWorld/{(EThirdWorldEnemyType)i}");
            _enemyManager.Enemys.Add(temp);
        }
        _enemyManager.Boss = await _addressableManager.GetAddressableAsset<GameObject>("ThirdWorld/Dragon");
    }

    async Task LoadRock()
    {
        _enemyManager.Missile = await _addressableManager.GetAddressableAsset<GameObject>("ThirdWorld/Rock");
    }
}