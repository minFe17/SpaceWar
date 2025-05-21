public class BulletUp : PassiveBase, IPassive
{
    int _bulletUp = 30;

    PassiveData IPassive.PassiveData { get => _passiveData; }

    void IPassive.AddPassive()
    {
        _playerData.MaxBullet += _bulletUp;
        _uiManager.IngameUI.ShowBullet();
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}