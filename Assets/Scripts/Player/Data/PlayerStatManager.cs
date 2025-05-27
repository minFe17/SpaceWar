using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PlayerStatManager : MonoBehaviour
{
    // ╫л╠шео
    List<PlayerInfoData> _playerStatDatas = new List<PlayerInfoData>();
    List<PlayerLevelData> _playerLevelDatas = new List<PlayerLevelData>();

    public List<PlayerInfoData> StatData { get => _playerStatDatas;  }
    public List<PlayerLevelData> LevelDatas { get => _playerLevelDatas; }

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
        _playerStatDatas.Add(DataSingleton<SoldierInfoData>.Instance);
        _playerStatDatas.Add(DataSingleton<WitchInfoData>.Instance);

        for(int i=0; i<_playerStatDatas.Count; i++)
            _playerStatDatas[i].Init();
    }

    void SetLevelData()
    {
        _playerLevelDatas.Add(DataSingleton<SoldierLevelData>.Instance);
        _playerLevelDatas.Add(DataSingleton<WitchLevelData>.Instance);
    }

    public void SetLock(int index)
    {
        _playerLevelDatas[index].IsUnlock = false;
    }
}
