using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    List<Enemy> _enemiesInRange = new List<Enemy>();

    public List<Enemy> EnemiesInRange { get => _enemiesInRange; }

    public void Init(EnemyController enemyController)
    {
        _enemiesInRange = enemyController.EnemyList;
    }

    public Enemy GetClosestEnemy()
    {
        Enemy target = null;
        float distance = float.MaxValue;
        Vector3 currentPosition = transform.position;

        foreach(Enemy closestEnemy in _enemiesInRange)
        {
            if(closestEnemy.transform != gameObject.transform)
            {
                Vector3 directionToTarget = closestEnemy.transform.position - currentPosition;
                float temp = directionToTarget.sqrMagnitude;

                if (temp < distance)
                {
                    distance = temp;
                    target = closestEnemy;
                }
            }
        }
        return target;
    }
}