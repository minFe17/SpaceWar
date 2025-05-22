using Utils;

public class BulletUp : PassiveData
{
    public override void AddPassive()
    {
        _playerData.MaxBullet *= 2;
        if (_uiManager == null)
            _uiManager = GenericSingleton<UIManager>.Instance;
        _uiManager.IngameUI.ShowBullet();
    }
}