using System.Collections;
using UnityEngine;
using Utils;

public class Turret : Enemy
{
    [SerializeField] EFirstWorldEnemyType _enemyType;
    [SerializeField] Transform _gun;
    [SerializeField] Transform _bulletPos;
    [SerializeField] GameObject _dieEffect;

    GameObject _bullet;
    bool _isAttack;

    void Start()
    {
        _bullet = _enemyManager.Missile;
    }

    public override void Init(EnemyController enemyController)
    {
        base.Init(enemyController);
        StartCoroutine(AttackRoutine());
    }

    void Update()
    {
        LookTarget();
        FreezePos();
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
            _enemyController.EnemyList.Remove(this);
        }
    }

    public override void Die()
    {
        _gun.localEulerAngles = new Vector3(60, _gun.eulerAngles.y, _gun.eulerAngles.z);
        _coinManager.MakeCoin(transform.position);
        GenericSingleton<GameManager>.Instance.AddKillEnemy();
        _player.Vampirism();
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