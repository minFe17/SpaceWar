using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField] GameObject _gun;
    [SerializeField] float _attackDelay;

    Transform _target;
    EnemyController _enemyController;

    int _curHp;
    bool _isAttack;

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
            _gun.transform.LookAt(_target.position);
    }

    public void TakeDamage(int damage)
    {
        _curHp -= damage;
        if (_curHp <= 0)
        {
            //파괴(이펙트)
            //파괴이펙트 없어지면 터렛 없애기
            Destroy(this.gameObject);
            _enemyController._enemyList.Remove(this.gameObject);
        }
    }

    IEnumerator AttackRoutine()
    {

        //바라보고 몇 초 뒤에 공격(코루틴)(공격 전 범위 알려주기)
        yield return new WaitForSeconds(_attackDelay);
        _isAttack = true;
        yield return new WaitForSeconds(1f);
        //미사일 소환하거나 등등 (어택)
        yield return new WaitForSeconds(_attackDelay);
        _isAttack = false;
    }
}
