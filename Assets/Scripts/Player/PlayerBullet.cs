using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
