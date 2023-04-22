using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // 싱글톤
    int _maxHp = 10;
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    int _curHp;
    public int CurHp { get { return _curHp; } set { _curHp = value; } }

    int _maxAmmo = 30;
    public int MaxAmmo { get { return _maxAmmo; } set { _maxAmmo = value; } }
    int _curAmmo;
    public int CurAmmo { get { return _curAmmo; } set { _curAmmo = value; } }

    float _moveSpeed = 5f;
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    float _splintSpeed = 5f;
    public float SplintSpeed {  get { return _splintSpeed; } }

    EShotModeType _shotMode;
    public EShotModeType ShotMode { get { return _shotMode; } set { _shotMode = value; } }
    bool _unlockBurstMode;
    bool _unlockAutoMode;

    int _money = 30;
    public int Money { get { return _money; } set { _money = value; } }

    public void SettingPlayerData()
    {
        //데이터 읽어서 널이면 초기값
        // 널이 아니면 데이터 세팅
        _curAmmo = _maxAmmo;
        _curHp = _maxHp;
        _shotMode = EShotModeType.Single;

    }


}
