using UnityEngine;
using Utils;

public class MiniScavenger : Scavenger
{
    GameObject _otherScavenger;

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
        if(OtherScavengerIsDie == false)
            _otherScavenger.GetComponent<MiniScavenger>().OtherScavengerIsDie = _isDie;

        _coinManager.MakeCoin(transform.position);
        GenericSingleton<GameManager>.Instance.AddKillEnemy();
        _player.Vampirism();
        _enemyController.EnemyList.Remove(this);
        Destroy(this.gameObject);

        if (OtherScavengerIsDie == true)
        {
            GenericSingleton<UIManager>.Instance.IngameUI.HideMiniBossHpBar();
            for (int i = 0; i < _enemyController.EnemyList.Count; i++)
            {
                _enemyController.EnemyList[0].RemoveEnemy();
            }
            GenericSingleton<GameManager>.Instance.Portal.SetActive(true);
        }
    }
}