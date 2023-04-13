using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Enemy : MonoBehaviour
{
    public int _maxHp;
    public float _attackDelay;

    protected EnemyController _enemyController;
    protected Transform _target;
    protected Player _player;

    protected int _curHp;
    protected bool _isDie;

    protected List<GameObject> _coinList = new List<GameObject>();
    // 나중에 코인을 리스트로 저장하기

    public virtual void Init(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _target = GenericSingleton<EnemyManager>.Instance.Target;
        _player = GenericSingleton<EnemyManager>.Instance.Target.GetComponent<Player>();
        _curHp = _maxHp;
        _enemyController._enemyList.Add(this);
        AddCoinList();
    }

    protected void AddCoinList()
    {
        _coinList.Add(Resources.Load("Prefabs/Coins/GoldCoin") as GameObject);
        _coinList.Add(Resources.Load("Prefabs/Coins/SilverCoin") as GameObject);
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
            GenericSingleton<GameManager>.Instance.AddKillEnemy();
            Destroy(this.gameObject, 1f);
            _enemyController._enemyList.Remove(this);
        }
    }

    protected void MakeMoney()
    {
        int random = Random.Range(0, _coinList.Count);
        GameObject coin = Instantiate(_coinList[random]);
        coin.transform.position = transform.position;
    }

    protected virtual IEnumerator AttackRoutine()
    {
        yield return null;
    }
}
