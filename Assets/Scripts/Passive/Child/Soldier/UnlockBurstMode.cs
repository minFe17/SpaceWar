public class UnlockBurstMode : PassiveData
{
    public override void AddPassive()
    {
        _playerData.UnlockFirstSkill = true;
    }
}