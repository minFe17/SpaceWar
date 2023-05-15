using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // �̱���
    int _maxHp = 10;
    public int MaxHp { get => _maxHp; set => _maxHp = value;  }
    int _curHp;
    public int CurHp { get => _curHp; set => _curHp = value;  }

    int _maxBullet = 30;
    public int MaxBullet { get => _maxBullet; set => _maxBullet = value;  }
    int _curBullet;
    public int CurBullet { get => _curBullet; set => _curBullet = value; }

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
        // ������ �б�
        // �����Ͱ� ���̸� �ʱ�ȭ
        // ���� �ƴϸ� ������ ����
        _curBullet = _maxBullet;
        _curHp = _maxHp;
        _shotMode = EShotModeType.Single;

    }
}
