using System.Collections.Generic;
using UnityEngine;

public abstract class WorldEnemyListManager
{
    protected List<GameObject> _worldEnemy = new List<GameObject>();
    public abstract void AddEnemyList();
}
