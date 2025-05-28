public class Witch : PlayerBase
{
    protected override void CharacterUpdate()
    {
        base.CharacterUpdate();
    }

    protected override void StartFirstSkillAttack()
    {
        Fire();
    }

    protected override void StartNormalAttack()
    {
        Fire();
    }

    protected override void StartSecondSkillAttack()
    {
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
