using UnityEngine;
using Utils;

public class Dragon : MovableEnemy
{
    [SerializeField] GameObject _breath;

    EDragonAttackType _dragonAttackType;

    public EDragonAttackType AttackType { get => _dragonAttackType; }

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }

    public override void Move()
    {
        if (!_isDie && !_isAttack)
        {
            _animator.SetBool("isWalk", true);
            _move = _target.position - transform.position;
            transform.Translate(_move.normalized * Time.deltaTime * _moveSpeed, Space.World);
        }
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
            _enemyController.EnemyList[0].RemoveEnemy();
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

    void BreathAttack()
    {
        _breath.SetActive(true);
        _breath.GetComponent<Breath>().Init(_player, this);
    }

    public void EndBreath()
    {
        _animator.SetBool("isEndBreathAttack", true);
    }

    void EndBreathAttack()
    {
        _breath.SetActive(false);
        _animator.SetBool("isEndBreathAttack", false);
    }

    void Shout()
    {
        int random = Random.Range(1, 4);
        for (int i = 0; i < random; i++)
            _enemyController.SpawnEnemy();
        _damage += 3;
    }
}