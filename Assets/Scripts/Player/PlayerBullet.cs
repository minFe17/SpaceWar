using UnityEngine;
using Utils;

public class PlayerBullet : Bullet
{
    [SerializeField] EPlayerPoolType _playerPoolType;
    [SerializeField] TrailRenderer _trailRenderer;
    ObjectPoolManager _objectPoolManager;

    void Update()
    {
        Move();
    }

    protected override void Remove()
    {
        _trailRenderer.Clear();
        if (_objectPoolManager == null)
            _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _objectPoolManager.Pull(EPlayerPoolType.Bullet, gameObject);
    }
}