using Utils;

public class BulletUp : PassiveData
{
    int _bulletUp = 30;

    public override void AddPassive()
    {
        _playerData.MaxBullet += _bulletUp;
        if (_uiManager == null)
            _uiManager = GenericSingleton<UIManager>.Instance;
        _uiManager.IngameUI.ShowBullet();
    }
}