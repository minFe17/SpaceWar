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

    void FixedUpdate() 
    {
        FreezeVelocity();
    }

    void Update()
    {
        LookTarget();
        Move();
    }
}
