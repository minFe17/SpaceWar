using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMissile : Bullet
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
