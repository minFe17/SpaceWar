using System.Collections;
using UnityEngine;

public abstract class MovableEnemy : Enemy
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _moveSpeed;

    protected Animator _animator;
    protected BoxCollider _collider;
    protected Rigidbody _rigidbody;

    protected Vector3 _move;

    protected bool _isAttack;
    protected bool _isHitted;
    protected bool _isMiss;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !_isAttack)
            StartCoroutine(AttackRoutine());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isMiss = true;
    }

    protected abstract IEnumerator AttackRoutine();
}
