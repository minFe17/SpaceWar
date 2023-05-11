using UnityEngine;

public class BearAttackArea : AttackArea
{
    [SerializeField] Bear _bear;

    public override void HitPlayer()
    {
        switch(_bear.BearAttackType)
        {
            case EBearAttackType.RightPunch:
                _bear.Player.TakeDamage(_bear.Damage);
                break;
            case EBearAttackType.Tap:
                _bear.Player.TakeDamage(_bear.Damage * 2);
                break;
        }
    }
}
