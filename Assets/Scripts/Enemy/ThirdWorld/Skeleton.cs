using UnityEngine;

public class Skeleton : MovableEnemy
{
    [SerializeField] GameObject _revivalCollider;
    bool _isRevival;

    void Awake()
    {
        _enemyType =EThirdWorldEnemyType.Skeleton;
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
        _isRevival = false;
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _revivalCollider.SetActive(false);
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
        if (!_isRevival)
            _animator.SetTrigger("doRevival");
        else
            base.Die();
    }

    void Revival()
    {
        _isRevival = true;
        _isDie = false;
        _curHp = _maxHp;
        _revivalCollider.SetActive(true);
    }
}