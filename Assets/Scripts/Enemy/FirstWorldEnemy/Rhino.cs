using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : MovableEnemy
{
    void Update()
    {
        LookTarget();
        Move();
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
        }
        else
        {
            _animator.SetBool("isMove", false);
            _isHitted = true;
            Invoke("Moveable", 1f);
        }
    }

    public override void Die()
    {
        base.Die();
        for(int i=0; i<_enemyController.EnemyList.Count; i++)
        {
            _enemyController.EnemyList[i].Die();
        }
    }

    void Moveable()
    {
        _isHitted = false;
        _animator.SetBool("isHit", false);
    }
}
