using UnityEngine;

public class Raptor : MovableEnemy
{
    [SerializeField] ESecondWorldEnemyType _enemyType;
    [SerializeField] SkinnedMeshRenderer _meshRenderer;

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
    }

    public override void Move()
    {
        if (!_isAttack && !_isDie && !_isHitted)
        {
            _animator.SetBool("isMove", true);
            _move = _target.position - transform.position;
            transform.Translate(_move.normalized * Time.deltaTime * _moveSpeed, Space.World);
            _meshRenderer.material = _enemyManager.RaptorMaterials[(int)ERaptorMaterialType.Cloaking];
        }
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;
        _curHp -= damage;
        _meshRenderer.material = _enemyManager.RaptorMaterials[(int)ERaptorMaterialType.Raptor];
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

    protected override void ReadyAttack()
    {
        base.ReadyAttack();
        _meshRenderer.material = _enemyManager.RaptorMaterials[(int)ERaptorMaterialType.Raptor];
    }
}