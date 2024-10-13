using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class EnemyManager : MonoBehaviour
{
    // ╫л╠шео
    List<IWorldEnemyListBase> _worldList = new List<IWorldEnemyListBase>();
    List<GameObject> _enemys = new List<GameObject>();

    AddressableManager _addressableManager;
    Transform _target;

    public Transform Target { get => _target; set => _target = value; }
    public List<GameObject> Enemys { get => _enemys; }
    public List<Material> RaptorMaterials { get; }

    public GameObject Boss { get; set; }
    public GameObject MiniBoss { get; set; }
    public GameObject Missile { get; set; }
    public GameObject Rock { get; set; }

    public async Task LoadAsset(EWorldType worldType)
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        if (_worldList.Count == 0)
            AddWorldList();
        await _worldList[(int)worldType].AddEnemyList(this, _addressableManager);
    }

    void AddWorldList()
    {
        _worldList.Add(new FirstWorld());
        _worldList.Add(new SecondWorld());
        _worldList.Add(new ThirdWorld());
    }

    public void ReleaseAsset(EWorldType worldType)
    {
        if (_enemys.Count == 0)
            return;

        _enemys.Clear();
        for (int i = 0; i < _enemys.Count; i++)
            _addressableManager.Release(_enemys[i]);

        _addressableManager.Release(Boss);
        _worldList[(int)worldType].ReleaseAsset();
    }
}