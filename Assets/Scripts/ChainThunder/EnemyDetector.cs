using System.Collections.Generic;
using UnityEngine;

// 각 몬스터들에게 넣기?
public class EnemyDetector : MonoBehaviour
{
    List<Enemy> _enemiesInRange = new List<Enemy>();

    public List<Enemy> EnemiesInRange { get => _enemiesInRange; }

    public Enemy GetClosestEnemy()
    {
        if(_enemiesInRange.Count <= 0)
            return null;
        Enemy target = null;
        float distance = float.MaxValue;
        Vector3 currentPosition = transform.position;

        foreach(Enemy closestEnemy in _enemiesInRange)
        {
            Vector3 directionToTarget = closestEnemy.transform.position - currentPosition;
            float temp = directionToTarget.sqrMagnitude;

            if(temp < distance)
            {
                distance = temp;
                target = closestEnemy;
            }
        }
        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemiesInRange.Count == 0)
                _enemiesInRange.Add(enemy);
            else if (!_enemiesInRange.Contains(enemy))
                _enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if(_enemiesInRange.Count > 0)
                _enemiesInRange.Remove(enemy);
        }
    }
}