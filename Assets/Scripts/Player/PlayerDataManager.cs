using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // 싱글톤
    int _maxHp = 10;
    int _maxBullet = 30;
    int _bulletDamage = 1;
    float _moveSpeed = 5f;
    float _splintSpeed = 5f;

    public EShotModeType ShotMode { get; set; }
    public Player Player { get; set; }
    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public int CurHp { get; set; }
    public int MaxBullet { get => _maxBullet; set => _maxBullet = value; }
    public int CurBullet { get; set; }
    public int BulletDamage { get => _bulletDamage; set => _bulletDamage = value; }
    public int Money { get; set; }
    public int BonusMoney { get; set; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float SplintSpeed { get => _splintSpeed; }
    public bool UnlockBurstMode { get; set; }
    public bool UnlockAutoMode { get; set; }
    public bool HPUpByMoney { get; set; }
    public bool Vampirism { get; set; }

    public void SettingPlayerData()
    {
        // 데이터 읽기
        // 데이터가 널이면 초기화
        // 널이 아니면 데이터 세팅
        CurBullet = _maxBullet;
        CurHp = _maxHp;
        ShotMode = EShotModeType.Single;

    }
}
