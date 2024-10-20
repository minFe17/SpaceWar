using UnityEngine;

public class FireStone : MovableEnemy
{
    [SerializeField] EThirdWorldEnemyType _enemyType;

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
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
            _animator.SetTrigger("doHit");
            _isHitted = true;
        }
    }
}