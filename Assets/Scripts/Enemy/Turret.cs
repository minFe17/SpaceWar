using System.Collections;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] Transform _gun;
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletPos;
    [SerializeField] GameObject _dieEffect;
    
    bool _isAttack;

    public override void Init(EnemyController enemyController, Transform target)
    {
        _enemyController = enemyController;
        _target = target;
        _curHp = _maxHp;
        _enemyController._enemyList.Add(this);
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        LookTarget();
    }

    public override void LookTarget()
    {
        if (!_isAttack && !_isDie)
            _gun.LookAt(_target.position);
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;
        if (_curHp <= 0)
        {
            _isDie = true;
            _dieEffect.SetActive(true);
            Invoke("Die", 1f);

            Destroy(this.gameObject, 2f);
            _enemyController._enemyList.Remove(this);
        }
    }

    public void Die()
    {
        _gun.localEulerAngles = new Vector3(60, _gun.eulerAngles.y, _gun.eulerAngles.z);
    }

    protected override IEnumerator AttackRoutine()
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
