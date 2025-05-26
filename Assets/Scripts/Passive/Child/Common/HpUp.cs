using Utils;

public class HpUp : PassiveData
{
    int _hpAmount = 10;

    public override void AddPassive()
    {
        _playerData.MaxHp += _hpAmount;
        _playerData.CurHp += _hpAmount;
        if (_uiManager == null)
            _uiManager = GenericSingleton<UIManager>.Instance;
        _uiManager.IngameUI.ShowHp();
    }
}