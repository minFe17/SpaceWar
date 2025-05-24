using UnityEngine;

[System.Serializable]
public class PlayerStatData
{
    // 데이터 싱글턴
    [SerializeField] int _maxHp;
    [SerializeField] int _maxBullet;
    [SerializeField] int _bulletDamage;
    [SerializeField] int _upgradeCost;

    public int MaxHp { get => _maxHp; }
    public int MaxBullet { get => _maxBullet; }
    public int BulletDamage { get => _bulletDamage; }
    public int UpgradeCost { get => _upgradeCost; }

    public PlayerStatData(int maxHp, int maxBullet, int bulletDamage, int upgradeCost)
    {
        _maxHp = maxHp;
        _maxBullet = maxBullet;
        _bulletDamage = bulletDamage;
        _upgradeCost = upgradeCost;
    }
}