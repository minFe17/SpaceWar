using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableEnemy : Enemy
{
    public int _damage;
    public float _moveSpeed;

    protected NavMeshAgent _nav;
    protected BoxCollider _collider;
    protected Rigidbody _rigidbody;

    protected bool _isAttack;

    void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _nav.speed = _moveSpeed;
    }

    public virtual void Move()
    {
        if(!_isAttack && !_isDie)
        {
            _nav.SetDestination(_target.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            StartCoroutine(AttackRoutine());
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
