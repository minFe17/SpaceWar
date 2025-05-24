using System.Collections.Generic;
using UnityEngine;

public class PlayerStatManager : MonoBehaviour
{
    // 싱글턴
    List<PlayerInfoData> _playerStatDatas = new List<PlayerInfoData>();
    List<PlayerLevelData> _playerLevelDatas = new List<PlayerLevelData>();

    public List<PlayerInfoData> StatData { get => _playerStatDatas;  }
    public List<PlayerLevelData> LevelDatas { get => _playerLevelDatas; }

    // 호출 필요
    public void Init()
    {
        if (_playerStatDatas.Count > 0)
            return;

        SetStatData();
        SetLevelData();
        // json 파일 읽기 필요
        // 파일 읽을 때 _playerStatDatas, _playerLevelDatas 매개변수로 줘야할듯
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
