using UnityEngine;

public class TurretMissile : Bullet
{
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    public override void Move()
    {
        _rigidbody.AddRelativeForce(Vector3.forward * _speed * Time.deltaTime, ForceMode.Impulse);
    }
}
