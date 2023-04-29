using System.Collections;
using UnityEngine;
using Utils;

public class Scavenger : MovableEnemy
{
    [SerializeField] GameObject _attackArea;

    public int Damage { get { return _damage; } }

    EAttackType _attackType;
    public EAttackType AttackType { get { return _attackType; } }

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

    public override void Init(EnemyController enemyController)
    {
        base.Init(enemyController);
        _attackArea.SetActive(false);
        GenericSingleton<UIManager>.Instance.IngameUI.ShowBossHpBar(_curHp, _maxHp);
    }

    public override void Move()
    {
        if (!_isDie && !_isAttack)
        {
            _animator.SetBool("isWalk", true);
            transform.Translate(_move.normalized * Time.deltaTime * _moveSpeed, Space.World);
        }
    }

    void OnAttackArea()
    {
        _attackArea.SetActive(true);
    }

    void OffAttackArea()
    {
        _attackArea.SetActive(false);
        _isAttack = false;
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
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
            _isHitted = true;
            _animator.SetTrigger("doHit");
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
    }

    protected override IEnumerator AttackRoutine()
    {
        if (!_isAttack)
        {
            _isAttack = true;
            RandomAttack();
            yield return null;
        }
    }

    void RandomAttack()
    {
        _attackType = (EAttackType)Random.Range(1, (int)EAttackType.Max);
        _animator.SetTrigger($"do{_attackType}");
    }
}

public enum EAttackType
{
    None,
    RightSlice,
    BothHands,
    Max,
}
