public class UnlockAutoMode : PassiveData
{
    public override void AddPassive()
    {
        _playerData.UnlockSecondSkill = true;
    }
}