public class GetMoneyUp : PassiveBase, IPassive
{
    int _bonusMoney = 10;

    PassiveData IPassive.PassiveData{get =>_passiveData;}
    void IPassive.AddPassive()
    {
        _playerDataManager.BonusMoney = _bonusMoney;
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}