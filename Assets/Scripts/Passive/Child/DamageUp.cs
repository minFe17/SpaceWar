public class DamageUp : PassiveData
{
    int _bulletDamage = 3;
    
    public override void AddPassive()
    {
        _playerData.BulletDamage += _bulletDamage;
    }
}