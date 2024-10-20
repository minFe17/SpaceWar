using UnityEngine;

public class PlayerBullet : Bullet
{
    [SerializeField] EPlayerPoolType _playerPoolType;

    void Update()
    {
        Move();
    }
}