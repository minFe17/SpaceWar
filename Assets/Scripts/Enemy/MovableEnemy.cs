using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableEnemy : Enemy
{
    public int _damage;
    public float _moveSpeed;

    protected BoxCollider _collider;
    protected Rigidbody _rigidbody;

    protected Vector3 _move;

    protected bool _isAttack;
    protected bool _isHitted;
    protected bool _isMiss;

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Move()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !_isAttack)
            StartCoroutine(AttackRoutine());
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
