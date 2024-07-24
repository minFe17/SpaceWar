using UnityEngine;
using Utils;

public class Scavenger : MovableEnemy
{
    EAttackType _attackType;

    public EAttackType AttackType { get { return _attackType; } }

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
        SplitBoss();
    }

    void SplitBoss()
    {
        GameObject temp = Resources.Load("Prefabs/Enemys/SecondWorld/MiniBoss") as GameObject;
        MiniScavenger firstMiniBoss = Instantiate(temp).GetComponent<MiniScavenger>();
        MiniScavenger secondMiniBoss = Instantiate(temp).GetComponent<MiniScavenger>();

        Vector3 firstBossPos = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z);
        Vector3 secondBossPos = new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z);

        InitSplitBoss(firstMiniBoss, secondMiniBoss, firstBossPos);
        InitSplitBoss(secondMiniBoss, firstMiniBoss, secondBossPos);

        GenericSingleton<UIManager>.Instance.IngameUI.CreateMiniBossHpBar(firstMiniBoss, secondMiniBoss);
    }

    void InitSplitBoss(MiniScavenger miniBoss, MiniScavenger otherBoss, Vector3 pos)
    {
        miniBoss.Spawn(otherBoss.gameObject);
        miniBoss.Init(_enemyController);
        miniBoss.transform.position = pos;
    }

    protected override void ReadyAttack()
    {
        _isAttack = true;
        RandomAttack();
    }

    void Shout()
    {
        int random = Random.Range(1, 6);
        for (int i = 0; i < random; i++)
            _enemyController.SpawnEnemy();
        _moveSpeed += 0.3f;
    }

    void RandomAttack()
    {
        _attackType = (EAttackType)Random.Range(1, (int)EAttackType.Max);
        _animator.SetTrigger($"do{_attackType}");
    }
}