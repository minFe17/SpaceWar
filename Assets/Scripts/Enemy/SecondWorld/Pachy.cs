using UnityEngine;

public class Pachy : MovableEnemy
{
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
            _isHitted = true;
            _animator.SetTrigger("doHit");
        }
    }

    protected override void ReadyAttack()
    {
        _animator.SetTrigger("doReady");
    }

    protected override void Attack()
    {
        _isAttack = true;
        _animator.SetTrigger("doAttack");
    }

    protected override void EndAttack()
    {
        _isAttack = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isAttack)
            _player.TakeDamage(_damage);
    }
}
