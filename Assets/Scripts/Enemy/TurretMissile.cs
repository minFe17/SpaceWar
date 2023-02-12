using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissile : Bullet
{

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _rigidbody.AddRelativeForce(Vector3.forward * _speed * Time.deltaTime, ForceMode.Impulse);
    }
}
