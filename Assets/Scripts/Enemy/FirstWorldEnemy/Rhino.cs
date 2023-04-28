using System.Collections;
using UnityEngine;

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
            _animator.SetBool("isMove", false);
            _isHitted = true;
            Invoke("Moveable", 1f);
        }
    }

    public override void Die()
    {
        base.Die();
        for (int i = 0; i < _enemyController.EnemyList.Count; i++)
        {
            _enemyController.EnemyList[i].Die();
        }
    }

    void Moveable()
    {
        _isHitted = false;
        _animator.SetBool("isHit", false);
    }

    protected override IEnumerator AttackRoutine()
    {
        _isAttack = true;
        _animator.SetBool("isMove", false);

        _attackType = (ERhinoAttackType)Random.Range(0, (int)ERhinoAttackType.Max);
        yield return new WaitForSeconds(_attackDelay / 2);
        _animator.SetBool($"is{_attackType}", true);
    }

    void Attack()
    {
        _attackArea.SetActive(true);
    }

    void Shout()
    {
        int random = Random.Range(1, 4);
        for (int i = 0; i < random; i++)
            _enemyController.SpawnEnemy();
    }

    void EndAttack()
    {
        _animator.SetBool($"is{_attackType}", false);
        if (_attackArea.activeSelf == true)
            _attackArea.SetActive(false);
        _isAttack = false;
    }
}

public enum ERhinoAttackType
{
    Attack,
    Shout,
    Rush,
    Max
}
