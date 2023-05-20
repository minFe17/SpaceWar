using UnityEngine;
using Utils;

public class BearAttackArea : AttackArea
{
    [SerializeField] Bear _bear;

    public override void HitPlayer()
    {
        switch(_bear.BearAttackType)
        {
            case EBearAttackType.RightPunch:
                GenericSingleton<PlayerDataManager>.Instance.Player.TakeDamage(_bear.Damage);
                break;
            case EBearAttackType.Tap:
                GenericSingleton<PlayerDataManager>.Instance.Player.TakeDamage(_bear.Damage * 2);
                break;
        }
    }
}
