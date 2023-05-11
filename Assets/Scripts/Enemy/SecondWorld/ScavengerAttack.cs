using UnityEngine;

public class ScavengerAttack : AttackArea
{
    [SerializeField] Scavenger _boss;

    public override void HitPlayer()
    {
        switch (_boss.AttackType)
        {
            case EAttackType.RightSlice:
                _boss.Player.TakeDamage(_boss.Damage);
                break;
            case EAttackType.BothHands:
                _boss.Player.TakeDamage(_boss.Damage * 2);
                break;
        }
    }
}
