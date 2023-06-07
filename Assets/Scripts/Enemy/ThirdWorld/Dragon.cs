using UnityEngine;
using Utils;

public class Dragon : MovableEnemy
{
    EDragonAttackType _dragonAttackType;
    public EDragonAttackType AttackType { get => _dragonAttackType; }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }

    public override void Init(EnemyController enemyController)
    {
        base.Init(enemyController);
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBossHpBar(_curHp, _maxHp);
    }

    public override void TakeDamage(int damage)
    {
        if(_isDie)
            return;

        _curHp -= damage;

        GenericSingleton<UIManager>.Instance.IngameUI.ShowBossHpBar(_curHp, _maxHp);

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
        base.Die();
        GenericSingleton<UIManager>.Instance.IngameUI.HideBossHpBar();
        for (int i = 0; i < _enemyController.EnemyList.Count; i++)
        {
            _enemyController.EnemyList[i].Die();
        }
        GenericSingleton<GameManager>.Instance.Portal.SetActive(true);
    }

    protected override void ReadyAttack()
    {
        _dragonAttackType = (EDragonAttackType)Random.Range(0, (int)EDragonAttackType.Max);
        _animator.SetTrigger($"do{_dragonAttackType}");
        _isAttack = true;
    }

    protected override void EndAttack()
    {
        if (_attackArea.activeSelf == true)
            _attackArea.SetActive(false);
        _isAttack = false;
    }

    void Shout()
    {
        int random = Random.Range(1, 4);
        for (int i = 0; i < random; i++)
            _enemyController.SpawnEnemy();
        _damage += 3;
    }
}

public enum EDragonAttackType
{
    BasicAttack,
    ClawAttack,
    Shout,
    Max,
}