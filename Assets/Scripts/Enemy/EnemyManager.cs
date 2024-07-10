using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyManager : MonoBehaviour
{
    // ½Ì±ÛÅæ
    Transform _target;
    public Transform Target { get => _target; set => _target = value; }

    List<WorldEnemyListBase> _worldList = new List<WorldEnemyListBase>();

    List<GameObject> _enemys = new List<GameObject>();
    public List<GameObject> Enemys
    {
        get
        {
            if (_enemys.Count == 0)
            {
                WorldEnemyList();
            }
            return _enemys;
        }
    }

    public void WorldEnemyList()
    {
        if (_worldList.Count == 0)
            AddWorldList();
        int stage = GenericSingleton<GameManager>.Instance.MapStage - 1;
        _enemys = _worldList[stage].AddEnemyList();
    }

    public void ClearWorldEnemy()
    {
        _enemys.Clear();
    }

    void AddWorldList()
    {
        _worldList.Add(new FirstWorld());
        _worldList.Add(new SecondWorld());
        _worldList.Add(new ThirdWorld());
    }
}