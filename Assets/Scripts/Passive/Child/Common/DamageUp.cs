using Utils;

public class DamageUp : IPassiveEffect
{
    int _bulletDamage = 3;

    void IPassiveEffect.AddPassive()
    {
        DataSingleton<PlayerData>.Instance.BulletDamage += _bulletDamage;
    }
}