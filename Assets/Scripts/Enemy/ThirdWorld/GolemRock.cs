using UnityEngine;

public class GolemRock : Bullet
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
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }
}
