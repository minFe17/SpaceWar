using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PlayerStatManager : MonoBehaviour
{
    // 싱글턴
    List<PlayerInfoData> _playerStatDatas = new List<PlayerInfoData>();
    List<PlayerLevelData> _playerLevelDatas = new List<PlayerLevelData>();

    public List<PlayerInfoData> StatData { get => _playerStatDatas;  }
    public List<PlayerLevelData> LevelDatas { get => _playerLevelDatas; }

    // 로비에서? 호출 필요
    public async Task Init()
    {
        if (_playerStatDatas.Count > 0)
            return;

        SetStatData();
        SetLevelData();
        await GenericSingleton<JsonManager>.Instance.ReadPlayerStatDataFile();
    }

    void SetStatData()
    {
        _playerStatDatas.Add(new SoldierInfoData());
        _playerStatDatas.Add(new WitchInfoData());

        for(int i=0; i<_playerStatDatas.Count; i++)
            _playerStatDatas[i].Init();
    }

    void SetLevelData()
    {
        _playerLevelDatas.Add(new SoldierLevelData());
        _playerLevelDatas.Add(new WitchLevelData());
    }

    public void SetLock(int index)
    {
        _playerLevelDatas[index].IsUnlock = false;
    }
}
