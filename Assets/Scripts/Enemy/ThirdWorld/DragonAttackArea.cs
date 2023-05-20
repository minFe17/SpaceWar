using UnityEngine;
using Utils;

public class DragonAttackArea : AttackArea
{
    [SerializeField] Dragon _dragon;

    public override void HitPlayer()
    {
        switch (_dragon.AttackType)
        {
            case EDragonAttackType.BasicAttack:
                GenericSingleton<PlayerDataManager>.Instance.Player.TakeDamage(_dragon.Damage);
                break;
            case EDragonAttackType.ClawAttack:
                GenericSingleton<PlayerDataManager>.Instance.Player.TakeDamage(_dragon.Damage * 3);
                break;
        }
    }
}
