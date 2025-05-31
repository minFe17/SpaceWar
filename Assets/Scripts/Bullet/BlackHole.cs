using System.Collections.Generic;
using UnityEngine;
using Utils;

public class BlackHole : MonoBehaviour
{
    [SerializeField] EBulletPoolType _bulletType;
    [SerializeField] float _lifeTime;
    [SerializeField] float _speed;
    [SerializeField] float _pullRadius;
    [SerializeField] float _pullForce;
    [SerializeField] float _damageInterval;
    [SerializeField] int _damageAmount;

    List<Enemy> _enemies;
    Dictionary<Enemy, float> _damageTimer = new Dictionary<Enemy, float>();

    EnemyManager _enemyManager;
    ObjectPoolManager _objectPoolManager;

    bool _isStop;

    void OnEnable()
    {
        _isStop = false;
        if (_enemyManager == null)
            _enemyManager = GenericSingleton<EnemyManager>.Instance;
        if (_enemyManager.EnemyController != null)
            _enemies = _enemyManager.EnemyController.EnemyList;
        Invoke("Remove", _lifeTime);
    }

    void FixedUpdate()
    {
        if (_enemies == null)
            return;
        foreach (Enemy enemy in _enemies)
        {
            if (enemy == null) continue;

            if (IsWithinPullRange(enemy))
            {
                PullEnemy(enemy);
                HandleDamage(enemy);
                enemy.IsPullBlackHole = true;
            }
            else
                enemy.IsPullBlackHole = false;
        }
    }

    void Update()
    {
        Move();
    }

    bool IsWithinPullRange(Enemy enemy)
    {
        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        return distance <= _pullRadius;
    }

    void Move()
    {
        if (_isStop)
            return;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    void PullEnemy(Enemy enemy)
    {
        Rigidbody rigidbody = enemy.GetComponent<Rigidbody>();
        if (rigidbody == null)
            return;

        Vector3 direction = (transform.position - rigidbody.position).normalized;
        rigidbody.AddForce(direction * _pullForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    void HandleDamage(Enemy enemy)
    {
        if (!_damageTimer.ContainsKey(enemy))
            _damageTimer[enemy] = 0f;

        _damageTimer[enemy] += Time.fixedDeltaTime;

        if (_damageTimer[enemy] >= _damageInterval)
        {
            _damageTimer[enemy] = 0f;
            enemy.TakeDamage(_damageAmount);
        }
    }

    void Remove()
    {
        if (_objectPoolManager == null)
            _objectPoolManager = GenericSingleton<ObjectPoolManager>.Instance;
        _objectPoolManager.Pull(_bulletType, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle"))
            _isStop = true;
        if (other.gameObject.CompareTag("Door"))
            _isStop = true;
    }
}