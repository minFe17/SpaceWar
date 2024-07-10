using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _maxHp;
    [SerializeField] protected float _attackDelay;

    protected Rigidbody _rigidbody;
    protected EnemyController _enemyController;
    protected Transform _target;
    protected Player _player;

    protected int _curHp;
    protected bool _isDie;

    protected List<GameObject> _coinList = new List<GameObject>();

    public virtual void Init(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _target = GenericSingleton<EnemyManager>.Instance.Target;
        _player = GenericSingleton<PlayerDataManager>.Instance.Player;
        _curHp = _maxHp;
        _enemyController.EnemyList.Add(this);
        AddCoinList();
        _rigidbody = GetComponent<Rigidbody>();
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
            Destroy(this.gameObject, 1f);
            _enemyController.EnemyList.Remove(this);
        }
    }

    protected void FreezePos()
    {
        _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y, 0);
    }

    public virtual void Die()
    {
        MakeMoney();
        GenericSingleton<GameManager>.Instance.AddKillEnemy();
        _player.Vampirism();
        RemoveEnemy();
    }

    public void RemoveEnemy()
    {
        Destroy(this.gameObject);
        _enemyController.EnemyList.Remove(this);
    }

    protected void MakeMoney()
    {
        int random = Random.Range(0, _coinList.Count);
        GameObject coin = Instantiate(_coinList[random]);
        coin.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DeadZone"))
            RemoveEnemy();
    }
}