using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EnemyManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }

    //WorldEnemyListManager _worldEnemyListManager = new FirstWorld();
    //public WorldEnemyListManager WorldEnemyListManager {  set { _worldEnemyListManager = value; } }

    List<WorldEnemyListManager> _worldList = new List<WorldEnemyListManager>();

    List<GameObject> _enemys = new List<GameObject>();
    public List<GameObject> Enemys
    {
        get
        {
            if (_enemys.Count == 0)
            {
                if (_worldList.Count == 0)
                    AddWorldList();
                _worldList[GenericSingleton<GameManager>.Instance.MapStage - 1].AddEnemyList();
            }
            return _enemys;
        }
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
    Max,
}

public enum EThirdWorldEnemyType
{
    Max,
}