using UnityEngine;
using Utils;

public class IceLance : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _curveAmount;
    [SerializeField] float _lifeTime;

    Transform _targetPos;
    Quaternion _initRotation;
    EnemyManager _enemyManager;
    ObjectPoolManager _objectPoolManager;

    EBulletPoolType _bulletType = EBulletPoolType.IceLance;

    bool _isCollision;

    public void Init()
    {
        _isCollision = false;
        if (_enemyManager == null)
            _enemyManager = GenericSingleton<EnemyManager>.Instance;

        if (_enemyManager.EnemyController == null)
            return;

        _targetPos = _enemyManager.EnemyController.GetClosestEnemy();
        if (_targetPos == null)
            return;

        Vector3 targetDir = (_targetPos.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDir);

        Quaternion curveRotation = Quaternion.AngleAxis(90f * _curveAmount, Vector3.up);

        _initRotation = curveRotation * targetRotation;
        transform.rotation = _initRotation;
    }

    void Update()
    {
        if(!_isCollision)
        {
            transform.position += transform.forward * _moveSpeed * Time.deltaTime;

            if (_targetPos == null)
                return;

            Vector3 targetDir = (_targetPos.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }

    void Remove()
    {
        if (_objectPoolManager == null)
            _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _objectPoolManager.Pull(_bulletType, gameObject);
    }

    public void IsCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            int damage = DataSingleton<PlayerData>.Instance.BulletDamage;
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        _isCollision = true;
        Invoke("Remove", 1f);
    }
}