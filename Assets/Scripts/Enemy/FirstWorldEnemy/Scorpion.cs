using UnityEngine;

public class Scorpion : MovableEnemy
{
    [SerializeField] EFirstWorldEnemyType _enemyType;

    void Awake()
    {
        _enemyType = EFirstWorldEnemyType.Scorpion;
    }

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }

    public override void TakeDamage(int damage)
    {
        if(_isDie)
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
        }
    }
}