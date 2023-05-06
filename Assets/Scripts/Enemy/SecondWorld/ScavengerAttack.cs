using UnityEngine;

public class ScavengerAttack : AttackArea
{
    [SerializeField] Scavenger _boss;

    public override void HitPlayer()
    {
        switch (_boss.AttackType)
        {
            case EAttackType.RightSlice:
                _enemy.Player.TakeDamage(_enemy.Damage);
                break;
            case EAttackType.BothHands:
                _enemy.Player.TakeDamage(_enemy.Damage * 2);
                break;
        }
    }
}
