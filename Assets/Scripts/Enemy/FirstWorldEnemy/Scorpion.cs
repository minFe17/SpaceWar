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
            Invoke("Moveable", 1f);
        }
    }

    void Moveable()
    {
        _isHitted = false;
    }

    protected override IEnumerator AttackRoutine()
    {
        _isAttack = true;
        yield return new WaitForSeconds(_attackDelay / 2);
        _animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.3f);
        if (!_isMiss)
        {
            _player.TakeDamage(_damage);
            yield return new WaitForSeconds(0.5f);
            _animator.SetBool("isAttack", false);
            yield return new WaitForSeconds(_attackDelay / 2);
        }
        else
        {
            _animator.SetBool("isAttack", false);
            yield return new WaitForSeconds(0.5f);
            _isMiss = false;
        }

        _isAttack = false;
    }
}
