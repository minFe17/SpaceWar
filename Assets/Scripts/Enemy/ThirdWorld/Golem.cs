using UnityEngine;

public class Golem : MovableEnemy
{
    GameObject _rockPrefab;
    GolemRock _rock;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _rockPrefab = Resources.Load("Prefabs/Enemys/ThirdWorld/Rock") as GameObject;
    }

    void Update()
    {
        LookTarget();
        Move();
    }

    public override void TakeDamage(int damage)
    {
        if (_isDie)
            return;

        _curHp -= damage;

        if (_curHp <= 0)
        {
            _animator.SetTrigger("doDie");
            _isDie = true;
        }
        else
        {
            _animator.SetTrigger("doHit");
            _isHitted = true;
        }
    }

    protected override void Attack()
    {
        GameObject rock = Instantiate(_rockPrefab);
        rock.transform.position = _attackArea.transform.position;
        rock.GetComponent<GolemRock>().enabled = false;
    }
    
    void ThrowRock()
    {
        _rock.enabled = true;
    }

    protected override void EndAttack()
    {
        _isAttack = false;
    }
}
