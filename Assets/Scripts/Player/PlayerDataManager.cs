using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerDataManager : MonoBehaviour
{
    // ╫л╠шео
    List<string> _passive = new List<string>();

    public List<string> Passive { get => _passive; set => _passive = value; }
    public EShootModeType ShootMode { get; set; }
    public Player Player { get; set; }

    public int MaxHp { get; set; }
    public int CurHp { get; set; }
    public int MaxBullet { get; set; }
    public int CurBullet { get; set; }
    public int BulletDamage { get; set; }
    public int Money { get; set; }
    public int BonusMoney { get; set; }
    public float MoveSpeed { get; set; }
    public float SplintSpeed { get; set; }

    public bool UnlockBurstMode { get; set; }
    public bool UnlockAutoMode { get; set; }
    public bool HPUpByMoney { get; set; }
    public bool Vampirism { get; set; }

    public void SettingPlayerData()
    {
        CsvController csvController = GenericSingleton<CsvController>.Instance;
        if (csvController.CheckDataFile())
        {
            csvController.ReadDataFile();
        }
        else
        {
            ResetData();
        }
        GenericSingleton<AudioClipManager>.Instance.PlayBGM(EBGMAudioType.BGM);
    }

    void ResetData()
    {
        if (_passive.Count != 0)
            _passive.Clear();
        MaxHp = 10;
        CurHp = MaxHp;
        MaxBullet = 30;
        CurBullet = MaxBullet;
        BulletDamage = 1;
        Money = 0;
        BonusMoney= 0;
        MoveSpeed = 5f;
        SplintSpeed = 5f;
        ShootMode = EShootModeType.Single;
        UnlockBurstMode = false;
        UnlockAutoMode = false;
        HPUpByMoney = false;
        Vampirism = false;
        GenericSingleton<GameManager>.Instance.ResetData();
    }
}