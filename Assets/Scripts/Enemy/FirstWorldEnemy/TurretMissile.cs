using UnityEngine;

public class TurretMissile : Bullet
{
    void Update()
    {
        Move();
    }

    public override void Move()
    {
        _rigidbody.AddRelativeForce(Vector3.forward * _speed * Time.deltaTime, ForceMode.Impulse);
    }
}
