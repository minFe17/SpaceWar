using UnityEngine;
using Utils;

public class ScavengerAttack : AttackArea
{
    [SerializeField] Scavenger _boss;

    public override void HitPlayer()
    {
        switch (_boss.AttackType)
        {
            case EAttackType.RightSlice:
                GenericSingleton<PlayerDataManager>.Instance.Player.TakeDamage(_boss.Damage);
                break;
            case EAttackType.BothHands:
                GenericSingleton<PlayerDataManager>.Instance.Player.TakeDamage(_boss.Damage * 2);
                break;
        }
    }
}
