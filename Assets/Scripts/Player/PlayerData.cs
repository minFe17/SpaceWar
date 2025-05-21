using UnityEngine;

public class PlayerData
{
    // 데이터 싱글턴
    [SerializeField] EShootModeType _shootMode;
    [SerializeField] int _maxHp;
    [SerializeField] int _curHp;
    [SerializeField] int _maxBullet;
    [SerializeField] int _curBullet;
    [SerializeField] int _bulletDamage;
    [SerializeField] int _money;
    [SerializeField] int _bonusMoney;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _splintSpeed;

    [SerializeField] bool _unlockBurstMode;
    [SerializeField] bool _unlockAutoMode;
    [SerializeField] bool _hpUpByMoney;
    [SerializeField] bool _vampirism;

    public EShootModeType ShootMode { get => _shootMode; set => _shootMode = value; }
    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public int CurHp { get => _curHp; set => _curHp = value; }
    public int MaxBullet { get => _maxBullet; set => _maxBullet = value; }
    public int CurBullet { get => _curBullet; set => _curBullet = value; }
    public int BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
    public int Money { get => _money; set => _money = value; }
    public int BonusMoney { get => _bonusMoney; set => _bonusMoney = value; }

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float SplintSpeed { get => _splintSpeed; set => _splintSpeed = value; }

    public bool UnlockBurstMode { get => _unlockBurstMode; set => _unlockBurstMode = value; }
    public bool UnlockAutoMode { get => _unlockAutoMode; set => _unlockAutoMode = value; }
    public bool HPUpByMoney { get => _hpUpByMoney; set => _hpUpByMoney = value; }
    public bool Vampirism { get => _vampirism; set => _vampirism = value; }

    public void ResetData()
    {
        _maxHp = 10;
        _curHp = _maxHp;
        _maxBullet = 30;
        _curBullet = _maxBullet;
        _bulletDamage = 1;
        _money = 0;
        _bonusMoney = 0;
        _moveSpeed = 5f;
        _splintSpeed = 5f;
        _shootMode = EShootModeType.Single;
        _unlockBurstMode = false;
        _unlockAutoMode = false;
        _hpUpByMoney = false;
        _vampirism = false;
    }
}