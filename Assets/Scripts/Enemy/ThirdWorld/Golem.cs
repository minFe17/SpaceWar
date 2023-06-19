using UnityEngine;

public class Golem : MovableEnemy
{
    GameObject _rockPrefab;
    GolemRock _rock;

    void Start()
    {
        _rockPrefab = Resources.Load("Prefabs/Enemys/ThirdWorld/Rock") as GameObject;
    }

    void Update()
    {
        LookTarget();
        Move();
        FreezePos();
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
        _rock = rock.GetComponent<GolemRock>();
        _rock.OnHand(_attackArea);
    }

    void ThrowRock()
    {
        _rock.Throw(_attackArea.transform);

    }

    protected override void EndAttack()
    {
        _isAttack = false;
    }
}
