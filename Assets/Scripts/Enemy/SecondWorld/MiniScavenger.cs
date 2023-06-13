using UnityEngine;
using Utils;

public class MiniScavenger : Scavenger
{
    GameObject _otherScavenger;

    public int CurHp { get => _curHp; }
    public int MaxHp {  get => _maxHp; }
    public bool IsDie { get => _isDie; }
    public bool OtherScavengerIsDie { get; set; }

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }

    public void Spawn(GameObject otherScavenger)
    {
        _otherScavenger = otherScavenger;
    }

    public override void Init(EnemyController enemyController)
    {
        _enemyController = enemyController;
        _target = GenericSingleton<EnemyManager>.Instance.Target;
        _player = GenericSingleton<PlayerDataManager>.Instance.Player;
        _curHp = _maxHp;
        _enemyController.EnemyList.Add(this);
        AddCoinList();
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
        if(OtherScavengerIsDie == false)
            _otherScavenger.GetComponent<MiniScavenger>().OtherScavengerIsDie = _isDie;
        
        MakeMoney();
        GenericSingleton<GameManager>.Instance.AddKillEnemy();
        _player.Vampirism();
        _enemyController.EnemyList.Remove(this);
        Destroy(this.gameObject);

        if (OtherScavengerIsDie == true)
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
