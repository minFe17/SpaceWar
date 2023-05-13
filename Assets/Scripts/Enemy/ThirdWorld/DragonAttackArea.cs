using UnityEngine;

public class DragonAttackArea : AttackArea
{
    [SerializeField] Dragon _dragon;

    public override void HitPlayer()
    {
        switch (_dragon.AttackType)
        {
            case EDragonAttackType.BasicAttack:
                _dragon.Player.TakeDamage(_dragon.Damage);
                break;
            case EDragonAttackType.ClawAttack:
                _dragon.Player.TakeDamage(_dragon.Damage * 3);
                break;
        }
    }
}
