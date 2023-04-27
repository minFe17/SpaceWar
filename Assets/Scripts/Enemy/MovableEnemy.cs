using UnityEngine;

public class MovableEnemy : Enemy
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _moveSpeed;

    protected BoxCollider _collider;
    protected Rigidbody _rigidbody;

    protected Vector3 _move;

    protected bool _isAttack;
    protected bool _isHitted;
    protected bool _isMiss;

    void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Move() { }

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
}
