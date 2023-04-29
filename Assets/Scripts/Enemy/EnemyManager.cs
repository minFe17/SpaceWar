using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyManager : MonoBehaviour
{
    // ½Ì±ÛÅæ
    Transform _target;
    public Transform Target { get => _target; set => _target = value; }

    List<WorldEnemyListManager> _worldList = new List<WorldEnemyListManager>();

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
        set
        {
            _enemys = value;
        }
    }

    public void WorldEnemyList()
    {
        if (_worldList.Count == 0)
            AddWorldList();
        _worldList[GenericSingleton<GameManager>.Instance.MapStage - 1].AddEnemyList();
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

public enum EFirstWorldEnemyType
{
    Turret,
    Scorpion,
    DeliveryRobot,
    Max,
}

public enum ESecondWorldEnemyType
{
    Zombie,
    Raptor,
    Pachy,
    Max,
}

public enum EThirdWorldEnemyType
{
    Max,
}