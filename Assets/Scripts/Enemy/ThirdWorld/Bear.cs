using UnityEngine;

public class Bear : MovableEnemy
{
    EBearAttackType _bearAttackType;
    public EBearAttackType BearAttackType { get => _bearAttackType; }

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

    protected override void ReadyAttack()
    {
        _isAttack = true;
        _bearAttackType = (EBearAttackType)Random.Range(0, (int)EBearAttackType.Max);
        _animator.SetTrigger($"do{_bearAttackType}");
    }
}

public enum EBearAttackType
{
    RightPunch,
    Tap,
    Max,
}
