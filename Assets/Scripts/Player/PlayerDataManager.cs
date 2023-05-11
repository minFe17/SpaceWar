using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // 싱글톤
    int _maxHp = 10;
    public int MaxHp { get => _maxHp; set => _maxHp = value;  }
    int _curHp;
    public int CurHp { get => _curHp; set => _curHp = value;  }

    int _maxAmmo = 30;
    public int MaxAmmo { get => _maxAmmo; set => _maxAmmo = value;  }
    int _curAmmo;
    public int CurAmmo { get => _curAmmo; set => _curAmmo = value; }

    float _moveSpeed = 5f;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    float _splintSpeed = 5f;
    public float SplintSpeed {  get => _splintSpeed;  }

    EShotModeType _shotMode;
    public EShotModeType ShotMode { get => _shotMode; set => _shotMode = value;  }
    bool _unlockBurstMode;
    bool _unlockAutoMode;

    int _money = 30;
    public int Money { get => _money; set => _money = value; }

    public void SettingPlayerData()
    {
        // 데이터 읽기
        // 데이터가 널이면 초기화
        // 널이 아니면 데이터 세팅
        _curAmmo = _maxAmmo;
        _curHp = _maxHp;
        _shotMode = EShotModeType.Single;

    }
}
