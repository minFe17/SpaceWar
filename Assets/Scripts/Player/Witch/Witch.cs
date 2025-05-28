public class Witch : PlayerBase
{
    protected override void CharacterUpdate()
    {
        base.CharacterUpdate();
    }

    protected override void StartNormalAttack()
    {
        _bulletType = EBulletPoolType.IceLance;
        Fire();
    }

    protected override void StartFirstSkillAttack()
    {
        //_bulletType = EBulletPoolType.ThunderBall;
        Fire();
    }
   
    protected override void StartSecondSkillAttack()
    {
        //_bulletType = EBulletPoolType.BlackHole;
        Fire();
    }

    void Fire()
    {
        if (CheckAttack() && !_isShoot)
        {
            _isShoot = true;
            _animator.SetTrigger("doAttack");
        }
    }
}
