using System.Collections.Generic;
using UnityEngine;

public abstract class WorldEnemyListBase
{
    protected List<GameObject> _worldEnemy = new List<GameObject>();
    public abstract List<GameObject> AddEnemyList();
}
