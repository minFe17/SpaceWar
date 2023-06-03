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

    public override void Move()
    {
        if (!_isDie && !_isHitted)
        {
            if(!_isAttack)
                _animator.SetBool("isMove", true);
            _move = _target.position - transform.position;
            transform.Translate(_move.normalized * Time.deltaTime * _moveSpeed, Space.World);
        }
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
        _isAttack = true;
        _animator.SetTrigger("doReady");
    }

    protected override void Attack()
    {
        _animator.SetTrigger("doAttack");
        _moveSpeed *= 2f;
    }

    protected override void EndAttack()
    {
        _isAttack = false;
        _moveSpeed /= 2f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && _isAttack)
            _player.TakeDamage(_damage);
    }
}
