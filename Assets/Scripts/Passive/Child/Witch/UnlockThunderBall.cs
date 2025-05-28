public class UnlockThunderBall : PassiveData
{
    public override void AddPassive()
    {
        _playerData.UnlockFirstSkill = true;
    }
}