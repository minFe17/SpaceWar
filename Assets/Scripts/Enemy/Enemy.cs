using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _maxHp;
    public float _attackDelay;

    protected GameObject _silverCoin;
    protected GameObject _goldCoin;
    protected Player _player;
    protected EnemyController _enemyController;
    protected Transform _target;

    public int _curHp;
    public bool _isDie;
    // 나중에 코인을 리스트로 저장하기

    public virtual void Init(Player player, EnemyController enemyController, Transform target)
    {
        _player = player;
        _enemyController = enemyController;
        _target = target;
        _curHp = _maxHp;
        _enemyController._enemyList.Add(this);
    }

    public virtual void LookTarget()
    {
        transform.LookAt(_target.position);
    }

    public virtual void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;
        if (_curHp <= 0)
        {
            _isDie = true;
            MakeMoney();
            Destroy(this.gameObject, 1f);
            _enemyController._enemyList.Remove(this);
        }
    }

    protected void MakeMoney()
    {
        int random = Random.Range(0, 2);
        if(random == 0)
        {
            GameObject coin = Instantiate(_silverCoin);
            coin.transform.position = transform.position;
        }
        else
        {
            GameObject coin = Instantiate(_goldCoin);
            coin.transform.position = transform.position;
        }
    }

    protected virtual IEnumerator AttackRoutine()
    {
        yield return null;
    }
}
