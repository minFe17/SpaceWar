using System.Collections;
using UnityEngine;
using Utils;

public class Zombie : MovableEnemy
{
    [SerializeField] GameObject _secondAttackArea;

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
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
            _isHitted = true;
            Invoke("Movable", 0.3f);
        }
    }

    protected override void Attack()
    {
        _attackArea.SetActive(true);
        _secondAttackArea.SetActive(true);
    }

    protected override void EndAttack()
    {
        _isAttack = false;
        _attackArea.SetActive(false);
        _secondAttackArea.SetActive(false);
    }
}
