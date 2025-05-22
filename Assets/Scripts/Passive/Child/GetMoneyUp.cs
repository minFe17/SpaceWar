public class GetMoneyUp : PassiveData
{
    int _bonusMoney = 10;

    public override void AddPassive()
    {
        _playerData.BonusMoney = _bonusMoney;
    }
}