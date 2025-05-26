using UnityEngine;
using Utils;

[System.Serializable]
public class PlayerData
{
    // 데이터 싱글턴
    [SerializeField] EPlayerType _playerType;
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

    [SerializeField] bool _unlockFirstSkill;
    [SerializeField] bool _unlockSecondSkill;
    [SerializeField] bool _hpUpByMoney;
    [SerializeField] bool _vampirism;

    public EPlayerType PlayerType { get => _playerType; set => _playerType = value; }
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

    public bool UnlockFirstSkill { get => _unlockFirstSkill; set => _unlockFirstSkill = value; }
    public bool UnlockSecondSkill { get => _unlockSecondSkill; set => _unlockSecondSkill = value; }
    public bool HPUpByMoney { get => _hpUpByMoney; set => _hpUpByMoney = value; }
    public bool Vampirism { get => _vampirism; set => _vampirism = value; }

    public void ResetData()
    {
        PlayerStatManager playerStatManager = GenericSingleton<PlayerStatManager>.Instance;
        PlayerInfoData data = playerStatManager.StatData[(int)_playerType];
        int level = playerStatManager.LevelDatas[(int)_playerType].Level;
        _maxHp = data.StatDataList[level].MaxHp;
        _curHp = _maxHp;
        _maxBullet = data.StatDataList[level].MaxBullet;
        _curBullet = _maxBullet;
        _bulletDamage = data.StatDataList[level].BulletDamage;
        _money = 0;
        _bonusMoney = 0;
        _moveSpeed = 5f;
        _splintSpeed = 5f;
        _shootMode = EShootModeType.Normal;
        _unlockFirstSkill = false;
        _unlockSecondSkill = false;
        _hpUpByMoney = false;
        _vampirism = false;
    }
}