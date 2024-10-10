using System.Threading.Tasks;
using UnityEngine;

public class SecondWorld : IWorldEnemyListBase
{
    EnemyManager _enemyManager;
    AddressableManager _addressableManager;

    async Task IWorldEnemyListBase.AddEnemyList(EnemyManager enemyManager, AddressableManager addressableManager)
    {
        _enemyManager = enemyManager;
        _addressableManager = addressableManager;

        if (enemyManager.Enemys.Count != 0)
            return;

        await LoadEnemy();
        await LoadRaptorMaterial();
    }

    void IWorldEnemyListBase.ReleaseAsset()
    {
        if (_enemyManager.MiniBoss != null)
            _addressableManager.Release(_enemyManager.MiniBoss);
        if (_enemyManager.RaptorMaterials.Count != 0)
            ReleaseRaptorMaterial();
    }

    async Task LoadEnemy()
    {
        for (int i = 0; i < (int)ESecondWorldEnemyType.Max; i++)
        {
            GameObject temp = await _addressableManager.GetAddressableAsset<GameObject>($"SecondWorld/{(ESecondWorldEnemyType)i}.prefab");
            _enemyManager.Enemys.Add(temp);
        }
        _enemyManager.Boss = await _addressableManager.GetAddressableAsset<GameObject>("SecondWorld/Scavenger.prefab");
        _enemyManager.MiniBoss = await _addressableManager.GetAddressableAsset<GameObject>("SecondWorld/MiniScavenger.prefab");
    }

    async Task LoadRaptorMaterial()
    {
        for (int i = 0; i < (int)ERaptorMaterialType.Max; i++)
        {
            Material temp = await _addressableManager.GetAddressableAsset<Material>($"{(ERaptorMaterialType)i}.prefab");
            _enemyManager.RaptorMaterials.Add(temp);
        }
    }

    void ReleaseRaptorMaterial()
    {
        for (int i = 0; i < _enemyManager.RaptorMaterials.Count; i++)
            _addressableManager.Release(_enemyManager.RaptorMaterials[i]);
    }
}