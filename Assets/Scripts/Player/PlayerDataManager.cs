using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // 싱글톤
    Dictionary<EPassiveType, int> _passive = new Dictionary<EPassiveType, int>();
    EShotModeType _shotMode;
    int _maxHp = 10;
    int _curHp;
    int _maxBullet = 30;
    int _curBullet;
    int _money = 30;
    float _moveSpeed = 5f;
    float _splintSpeed = 5f;
    bool _unlockBurstMode;
    bool _unlockAutoMode;

    public Dictionary<EPassiveType, int> Passive { get => _passive; set => _passive = value; }
    public EShotModeType ShotMode { get => _shotMode; set => _shotMode = value; }
    public int MaxHp { get => _maxHp; set => _maxHp = value; }
    public int CurHp { get => _curHp; set => _curHp = value; }
    public int MaxBullet { get => _maxBullet; set => _maxBullet = value; }
    public int CurBullet { get => _curBullet; set => _curBullet = value; }
    public int Money { get => _money; set => _money = value; }
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    public float SplintSpeed { get => _splintSpeed; }

    public void SettingPlayerData()
    {
        // 데이터 읽기
        // 데이터가 널이면 초기화
        // 널이 아니면 데이터 세팅
        _curBullet = _maxBullet;
        _curHp = _maxHp;
        _shotMode = EShotModeType.Single;

    }
}
