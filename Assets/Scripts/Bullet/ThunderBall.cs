using UnityEngine;
using Utils;

public class ThunderBall : Bullet
{
    [SerializeField] EBulletPoolType _bulletPoolType;
    [SerializeField] ChainThunder _chainThunder;
    ObjectPoolManager _objectPoolManager;

    void Update()
    {
        Move();
    }

    protected override void Remove()
    {
        if (_objectPoolManager == null)
            _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _objectPoolManager.Pull(_bulletPoolType, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            GenericSingleton<ChainThunder>.Instance.Init(other.GetComponent<EnemyDetector>());
        }
        Remove();
    }
}