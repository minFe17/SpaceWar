using System.Collections;
using UnityEngine;
using Utils;

public class Scorpion : MovableEnemy
{
    void Update()
    {
        LookTarget();
        Move();
    }

    public override void TakeDamage(int damage)
    {
        if(_isDie)
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
            Invoke("Movable", 1f);
        }
    }
}
