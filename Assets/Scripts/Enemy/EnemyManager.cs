using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class EnemyManager : MonoBehaviour
{
    // 싱글턴
    List<IEnemyList> _worldList = new List<IEnemyList>();
    List<GameObject> _enemys = new List<GameObject>();
    List<Material> _raptorMaterials = new List<Material>();

    AddressableManager _addressableManager;
    WorldManager _worldManager;
    Transform _target;

    public List<GameObject> Enemys { get => _enemys; }
    public List<Material> RaptorMaterials { get => _raptorMaterials; }
    public Transform Target { get => _target; set => _target = value; }

    public GameObject Boss { get; set; }
    public GameObject MiniBoss { get; set; }
    public GameObject Missile { get; set; }
    public GameObject Rock { get; set; }
    public EnemyController EnemyController { get; set; }

    void Awake()
    {
        _worldManager = GenericSingleton<WorldManager>.Instance;
    }

    public async Task LoadAsset()
    {
        if (_addressableManager == null)
            _addressableManager = GenericSingleton<AddressableManager>.Instance;

        if (_worldList.Count == 0)
            AddWorldList();
        await _worldList[(int)_worldManager.WorldType].AddEnemyList(this, _addressableManager);
    }

    void AddWorldList()
    {
        _worldList.Add(new FirstWorld());
        _worldList.Add(new SecondWorld());
        _worldList.Add(new ThirdWorld());
    }

    /// <summary>
    /// 현재 월드에서 사용한 몬스터 및 보스 리소스를 언로드
    /// </summary>
    public void ReleaseAsset()
    {
        // 언로드할 몬스터가 없으면 바로 종료
        if (_enemys.Count == 0)
            return;

        // 개별 몬스터 리소스 해제
        for (int i = 0; i < _enemys.Count; i++)
            _addressableManager.Release(_enemys[i]);

        // 리스트 초기화
        _enemys.Clear();

        // 보스 리소스 해제
        _addressableManager.Release(Boss);

        // 현재 월드에 등록된 리소스 해제
        _worldList[(int)_worldManager.WorldType].ReleaseAsset();
    }

    public Enum ConvertEnumToInt(int value)
    {
        return _worldList[(int)_worldManager.WorldType].ConvertEnumToInt(value);
    }
}