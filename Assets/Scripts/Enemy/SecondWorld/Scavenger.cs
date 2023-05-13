using UnityEngine;
using Utils;

public class Scavenger : MovableEnemy
{
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
        GameObject firstMiniBoss = Instantiate(temp);
        GameObject secondMiniBoss = Instantiate(temp);

        firstMiniBoss.GetComponent<MiniScavenger>().Spawn(secondMiniBoss);
        secondMiniBoss.GetComponent<MiniScavenger>().Spawn(firstMiniBoss);
        firstMiniBoss.transform.position = new Vector3(transform.position.x - 10f, transform.position.y, transform.position.z);
        secondMiniBoss.transform.position = new Vector3(transform.position.x + 10f, transform.position.y, transform.position.z);

        GenericSingleton<UIManager>.Instance.IngameUI.CreateMiniBossHpBar(firstMiniBoss, secondMiniBoss);
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

public enum EAttackType
{
    None,
    RightSlice,
    BothHands,
    Shout,
    Max,
}
