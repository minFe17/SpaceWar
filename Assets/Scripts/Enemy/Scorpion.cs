using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpion : MonoBehaviour
{
    [SerializeField] int _maxHp;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _attackDelay;

    Animator _animator;

    Transform _target;
    EnemyController _enemyController;

    int _curHp;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Init(EnemyController enemyController, Transform target)
    {
        _curHp = _maxHp;
        _enemyController = enemyController;
        _target = target;
        _enemyController._enemyList.Add(this.gameObject);
    }

    void Update()
    {
        LookTarget();
    }

    public void LookTarget()
    {
        transform.LookAt(_target.position);
    }


}
