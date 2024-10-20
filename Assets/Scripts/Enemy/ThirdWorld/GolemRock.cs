using UnityEngine;

public class GolemRock : Bullet
{
    GameObject _golemHand;
    bool _isThrow;

    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        if (_isThrow == true)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
            transform.Translate(Vector3.right * (_speed / 2) * Time.deltaTime);
        }
        else
        {
            transform.position = _golemHand.transform.position;
        }
    }

    public void OnHand(GameObject golemHand)
    {
        _isThrow = false;
        _golemHand = golemHand;
    }

    public void Throw(Transform golemHand)
    {
        transform.rotation = golemHand.rotation;
        _isThrow = true;
    }
}