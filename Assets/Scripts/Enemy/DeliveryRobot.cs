using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryRobot : MovableEnemy
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        LookTarget();
        Move();
    }

    public override void Move()
    {
        if(!_isAttack && !_isDie)
        {
            //nav mesh agent ���ְ� �ٸ� ���
            _animator.SetBool("isMove", true);
        }
    }

    protected override IEnumerator AttackRoutine()
    {
        // _isAttack = true;
        yield return null;
    }
}
