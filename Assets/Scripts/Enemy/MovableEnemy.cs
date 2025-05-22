using UnityEngine;

public class MovableEnemy : Enemy
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected GameObject _attackArea;

    protected Animator _animator;

    protected Vector3 _move;

    protected bool _isAttack;
    protected bool _isHitted;

    public int Damage { get => _damage; }

    public override void Init(EnemyController enemyController)
    {
        base.Init(enemyController);
        _isAttack = false;
        _isHitted = false;
        _animator = GetComponent<Animator>();
    }

    public virtual void Move()
    {
        if (!_isAttack && !_isDie && !_isHitted)
        {
            _animator.SetBool("isMove", true);
            _move = _target.position - transform.position;
            transform.Translate(_move.normalized * Time.deltaTime * _moveSpeed, Space.World);
        }
    }

    protected virtual void Movable()
    {
        _isHitted = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_isAttack)
            ReadyAttack();
    }

    protected virtual void ReadyAttack()
    {
        _isAttack = true;
        _animator.SetTrigger("doAttack");
    }

    protected virtual void Attack()
    {
        if (_isDie)
            return;
        _attackArea.SetActive(true);
    }

    protected virtual void EndAttack()
    {
        _isAttack = false;
        _attackArea.SetActive(false);
    }
}