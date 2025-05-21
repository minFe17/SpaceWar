public class HpUp : PassiveBase, IPassive
{
    int _hpAmount = 10;

    PassiveData IPassive.PassiveData { get => _passiveData; }

    void IPassive.AddPassive()
    {
        _playerData.MaxHp += _hpAmount;
        _playerData.CurHp += _hpAmount;
        _uiManager.IngameUI.ShowHp();
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}