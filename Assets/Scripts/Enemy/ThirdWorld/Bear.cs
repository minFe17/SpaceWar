using UnityEngine;

public class Bear : MovableEnemy
{
    [SerializeField] EThirdWorldEnemyType _enemyType;

    EBearAttackType _bearAttackType;
    public EBearAttackType BearAttackType { get => _bearAttackType; }

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