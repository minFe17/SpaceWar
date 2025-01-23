using UnityEngine;
using Utils;

public class Rhino : MovableEnemy
{
    ERhinoAttackType _attackType;

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }

    public override void Move()
    {
        if (!_isAttack && !_isDie)
        {
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
        _enemyController.EnemyList.Remove(this);
        _coinManager.MakeCoin(transform.position);
        GenericSingleton<GameManager>.Instance.AddKillEnemy();
        _player.Vampirism();
        GenericSingleton<UIManager>.Instance.IngameUI.HideBossHpBar();
        for (int i = 0; i < _enemyController.EnemyList.Count; i++)
        {
            _enemyController.EnemyList[i].Die();
        }
        GenericSingleton<GameManager>.Instance.Portal.SetActive(true);
        Destroy(this.gameObject);
    }

    protected override void ReadyAttack()
    {
        _isAttack = true;
        _attackType = (ERhinoAttackType)Random.Range(0, (int)ERhinoAttackType.Max);
        _animator.SetTrigger($"do{_attackType}");
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
    }
}