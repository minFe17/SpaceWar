public class DamageUp : PassiveBase, IPassive
{
    int _bulletDamage = 3;

    PassiveData IPassive.PassiveData { get => _passiveData; }

    void IPassive.AddPassive()
    {
        _playerDataManager.BulletDamage += _bulletDamage;
    }

    void IPassive.SetPassiveData(PassiveData passiveData)
    {
        SetPassiveData(passiveData);
    }
}