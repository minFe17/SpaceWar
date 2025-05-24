using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // �̱���
    List<PlayerInfoData> _playerStatDatas = new List<PlayerInfoData>();
    List<PlayerLevelData> _playerLevelDatas = new List<PlayerLevelData>();

    public List<PlayerInfoData> StatData { get => _playerStatDatas;  }
    public List<PlayerLevelData> LevelDatas { get => _playerLevelDatas; }

    // ȣ�� �ʿ�
    public void Init()
    {
        if (_playerStatDatas.Count > 0)
            return;

        SetStatData();
        SetLevelData();
        // json ���� �б� �ʿ�
        // ���� ���� �� _playerStatDatas, _playerLevelDatas �Ű������� ����ҵ�
    }

    void SetStatData()
    {
        _playerStatDatas.Add(new SoldierInfoData());
        _playerStatDatas.Add(new WitchInfoData());
    }

    void SetLevelData()
    {
        _playerLevelDatas.Add(new SoldierLevelData());
        _playerLevelDatas.Add(new WitchLevelData());
    }
}
