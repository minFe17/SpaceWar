using UnityEngine;
using Utils;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _maxHp;
    [SerializeField] protected float _attackDelay;

    protected EnemyController _enemyController;
    protected Rigidbody _rigidbody;
    protected EnemyManager _enemyManager;
    protected CoinManager _coinManager;
    protected Transform _target;
    protected Player _player;

    protected int _curHp;
    protected bool _isDie;

    public int CurHp { get => _curHp; }
    public int MaxHp { get => _maxHp; }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyManager = GenericSingleton<EnemyManager>.Instance;
        _coinManager = GenericSingleton<CoinManager>.Instance;
        _player = GenericSingleton<PlayerDataManager>.Instance.Player;
    }

    public virtual void Init(EnemyController enemyController)
    {
        _isDie = false;
        _curHp = _maxHp;
        _enemyController = enemyController;
        _enemyController.EnemyList.Add(this);
        _target = GenericSingleton<EnemyManager>.Instance.Target;
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
            _coinManager.MakeCoin(transform.position);
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
        _coinManager.MakeCoin(transform.position);
        GenericSingleton<GameManager>.Instance.AddKillEnemy();
        _player.Vampirism();
        RemoveEnemy();
    }

    public void RemoveEnemy()
    {
        Destroy(this.gameObject);
        _enemyController.EnemyList.Remove(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
            RemoveEnemy();
    }
}