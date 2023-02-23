using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _maxHp;
    public float _attackDelay;

    protected Transform _target;
    protected EnemyController _enemyController;

    protected int _curHp;
    protected bool _isDie;

    public virtual void Init(EnemyController enemyController, Transform target)
    {
        _enemyController = enemyController;
        _target = target;
        _curHp = _maxHp;
        _enemyController._enemyList.Add(this.gameObject);
    }

    public virtual void LookTarget()
    {
        transform.LookAt(_target.position);
    }

    public virtual void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;
        if (_curHp <= 0)
        {
            _isDie = true;
        }
        Destroy(this.gameObject, 2f);
        _enemyController._enemyList.Remove(this.gameObject);
    }
}
