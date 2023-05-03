using UnityEngine;
using Utils;

public class MiniScavenger : Scavenger
{
    GameObject _otherScavenger;

    public int CurHp { get => _curHp; }
    public int MaxHp {  get => _maxHp; }
    public bool IsDie { get => _isDie; }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LookTarget();
        Move();
    }

    public void Spawn(GameObject otherScavenger)
    {
        _otherScavenger = otherScavenger;
        Init(_enemyController);
    }

    public override void Init(EnemyController enemyController)
    {
        base.Init(enemyController);
        _attackArea.SetActive(false);
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;

        GenericSingleton<UIManager>.Instance.IngameUI.ShowMiniBossHpBar();

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
        }
        else
        {
            _isHitted = true;
            _animator.SetTrigger("doHit");
        }
    }

    public override void Die()
    {
        base.Die();
        if(_otherScavenger.GetComponent<MiniScavenger>().IsDie == true)
        {
            GenericSingleton<UIManager>.Instance.IngameUI.HideMiniBossHpBar();
            for (int i = 0; i < _enemyController.EnemyList.Count; i++)
            {
                _enemyController.EnemyList[i].Die();
            }
            GenericSingleton<GameManager>.Instance.Portal.SetActive(true);
        }
    }
}
