using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : MovableEnemy
{
    Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate() 
    {
        FreezeVelocity();
    }

    void Update()
    {
        LookTarget();
        Move();
    }

    protected override IEnumerator AttackRoutine()
    {
        _isAttack = true;
        yield return null;
    }
}
