using UnityEngine;

[System.Serializable]
public class PlayerStatData
{
    // ������ �̱���
    [SerializeField] int _maxHp;
    [SerializeField] int _maxBullet;
    [SerializeField] int _bulletDamage;
    [SerializeField] int _upgradeCost;

    public int MaxHp { get => _maxHp; }
    public int MaxBullet { get => _maxBullet; }
    public int BulletDamage { get => _bulletDamage; }
    public int UpgradeCost { get => _upgradeCost; }
}