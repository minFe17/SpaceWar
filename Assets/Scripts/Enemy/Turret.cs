using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform _gun;
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletPos;
    [SerializeField] GameObject _dieEffect;
    [SerializeField] int _maxHp;
    [SerializeField] float _attackDelay;

    Transform _target;
    EnemyController _enemyController;

    int _curHp;
    bool _isAttack;
    bool _isDie;

    void Start()
    {

    }

    public void Init(EnemyController enemyController, Transform target)
    {
        _curHp = _maxHp;
        _enemyController = enemyController;
        _target = target;
        _enemyController._enemyList.Add(this.gameObject);
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        LookTarget();
        //Enemy 상속받기? (만들고 상속할지 말지)

    }

    public void LookTarget()
    {
        if (!_isAttack)
            _gun.LookAt(_target.position);
    }

    public void TakeDamage(int damage)
    {
        _curHp -= damage;
        if (_curHp <= 0)
        {
            _isDie = true;
            _dieEffect.SetActive(true);
            Invoke("Die", 1f);

            Destroy(this.gameObject, 2f);
            _enemyController._enemyList.Remove(this.gameObject);
        }
    }

    public void Die()
    {
        _gun.localEulerAngles = new Vector3(60, _gun.eulerAngles.y, _gun.eulerAngles.z);
    }

    IEnumerator AttackRoutine()
    {
        while (!_isDie)
        {
            yield return new WaitForSeconds(_attackDelay);
            _isAttack = true;
            yield return new WaitForSeconds(1f);
            GameObject bullet = Instantiate(_bullet);
            bullet.transform.position = _bulletPos.position;
            bullet.transform.rotation = _gun.rotation;
            yield return new WaitForSeconds(_attackDelay);
            _isAttack = false;

        }

    }
}
