using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject _gun;
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _bulletPos;
    [SerializeField] int _maxHp;
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
        //Enemy ��ӹޱ�? (����� ������� ����)

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
            //�ı�(����Ʈ)
            //�ı�����Ʈ �������� �ͷ� ���ֱ�
            Destroy(this.gameObject);
            _enemyController._enemyList.Remove(this.gameObject);
        }
    }

    IEnumerator AttackRoutine()
    {

        yield return new WaitForSeconds(_attackDelay);
        _isAttack = true;
        yield return new WaitForSeconds(1f);
        GameObject bullet = Instantiate(_bullet);
        bullet.transform.position = _bulletPos.position;
        bullet.transform.LookAt(_target.position);
        yield return new WaitForSeconds(_attackDelay);
        _isAttack = false;
    }
}
