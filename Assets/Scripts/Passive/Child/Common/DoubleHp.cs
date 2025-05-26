using Utils;

public class DoubleHp : PassiveData
{
    public override void AddPassive()
    {
        _playerData.MaxHp *= 2;
        _playerData.CurHp *= 2;
        if (_uiManager == null)
            _uiManager = GenericSingleton<UIManager>.Instance;
        _uiManager.IngameUI.ShowHp();
    }
}