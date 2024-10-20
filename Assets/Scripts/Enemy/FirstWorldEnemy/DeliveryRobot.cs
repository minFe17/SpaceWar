using UnityEngine;

public class DeliveryRobot : MovableEnemy
{
    [SerializeField] EFirstWorldEnemyType _enemyType;

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
            _animator.SetBool("isMove", false);
            _animator.SetBool("isHit", true);
            _isHitted = true;
            Invoke("Movable", 1f);
        }
    }

    protected override void Movable()
    {
        _isHitted = false;
        _animator.SetBool("isHit", false);
    }
}