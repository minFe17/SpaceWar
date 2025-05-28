public class UnlockBlackHole : PassiveData
{
    public override void AddPassive()
    {
        _playerData.UnlockSecondSkill = true;
    }
}