using UnityEngine;

public class Skeleton : MovableEnemy
{
    bool _isRevival;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

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
            _animator.SetTrigger("doHit");
            _isHitted = true;
        }
    }

    public override void Die()
    {
        if (!_isRevival)
            _animator.SetTrigger("doRevival");
        else
            base.Die();
    }

    void Revival()
    {
        _isDie = false;
        _curHp = _maxHp;
    }
}
