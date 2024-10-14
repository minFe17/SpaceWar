public class HpUp : PassiveBase, IPassive
{
    int _hpAmount = 10;

    PassiveData IPassive.PassiveData { get => _passiveData; }

    void IPassive.AddPassive()
    {
        _playerDataManager.MaxHp += _hpAmount;
        _playerDataManager.CurHp += _hpAmount;
        _uiManager.IngameUI.ShowHp();
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}