using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableEnemy : Enemy
{
    public int _damage; 

    protected NavMeshAgent _nav;
    protected Rigidbody _rigidbody;

    void Awake()
    {
        _nav = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        _nav.SetDestination(_target.position);
    }

    public void FreezeVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    // Attack()
}
