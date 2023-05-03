using System.Collections;
using UnityEngine;
using Utils;

public class Rhino : MovableEnemy
{
    [SerializeField] GameObject _attackArea;

    ERhinoAttackType _attackType;

    public Player Player { get => _player; }
    public int Damage { get => _damage; }

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

    protected override IEnumerator AttackRoutine()
    {
        _isAttack = true;
        _animator.SetBool("isMove", false);

        _attackType = (ERhinoAttackType)Random.Range(0, (int)ERhinoAttackType.Max);
        yield return new WaitForSeconds(_attackDelay);
        _animator.SetBool($"is{_attackType}", true);
    }

    void Attack()
    {
        _attackArea.SetActive(true);
    }

    void EndAttack()
    {
        _animator.SetBool($"is{_attackType}", false);
        if (_attackArea.activeSelf == true)
            _attackArea.SetActive(false);
        _isAttack = false;
        Invoke("MoveAgain", _attackDelay);
    }

    void Shout()
    {
        int random = Random.Range(1, 4);
        for (int i = 0; i < random; i++)
            _enemyController.SpawnEnemy();
    }
}

public enum ERhinoAttackType
{
    Attack,
    Shout,
    Rush,
    Max
}
