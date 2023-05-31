using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerDataManager : MonoBehaviour
{
    // ΩÃ±€≈Ê
    List<string> _passive = new List<string>();

    int _maxHp = 10;
    int _maxBullet = 30;
    int _bulletDamage = 1;
    float _moveSpeed = 5f;
    float _splintSpeed = 5f;

    public List<string> Passive { get => _passive; set => _passive = value; }
    public EShotModeType ShotMode { get; set; }
    public Player Player { get; set; }

    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public int MaxBullet { get => _maxBullet; set => _maxBullet = value; }
    public int BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float SplintSpeed { get => _splintSpeed; set => _splintSpeed = value; }
    public int CurHp { get; set; }
    public int CurBullet { get; set; }
    public int Money { get; set; }
    public int BonusMoney { get; set; }

    public bool UnlockBurstMode { get; set; }
    public bool UnlockAutoMode { get; set; }
    public bool HPUpByMoney { get; set; }
    public bool Vampirism { get; set; }

    public void SettingPlayerData()
    {
        CsvController csvController = GenericSingleton<CsvController>.Instance;
        if (csvController.CheckDataFile())
            csvController.ReadDataFile();
        else
        {
            CurBullet = _maxBullet;
            CurHp = _maxHp;
            ShotMode = EShotModeType.Single;
        }
    }
}
