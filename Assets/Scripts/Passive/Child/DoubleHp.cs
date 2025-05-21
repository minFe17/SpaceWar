public class DoubleHp : PassiveBase, IPassive
{
    PassiveData IPassive.PassiveData { get => _passiveData; }

    void IPassive.AddPassive()
    {
        _playerData.MaxHp *= 2;
        _playerData.CurHp *= 2;
        _uiManager.IngameUI.ShowHp();
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}