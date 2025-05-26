using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;

public class PassiveManager : MonoBehaviour
{
    // ╫л╠шео
    JsonManager _jsonManager;
    CommonPassiveData _commonPassiveData = DataSingleton<CommonPassiveData>.Instance;
    List<PlayerPassiveData> _playerPassiveDatas = new List<PlayerPassiveData>();

    List<PassiveData> _allPassiveData;

    public List<PassiveData> AllPassiveData { get => _allPassiveData; }
    public List<PlayerPassiveData> PlayerPassiveDatas { get => _playerPassiveDatas; }

    async Task ReadData()
    {
        if (_jsonManager == null)
            _jsonManager = GenericSingleton<JsonManager>.Instance;
        await _jsonManager.ReadPassiveData();
    }

    public async Task Init()
    {
        if (_playerPassiveDatas.Count == 0)
        {
            _playerPassiveDatas.Add(DataSingleton<SoldierPassiveData>.Instance);
            _playerPassiveDatas.Add(DataSingleton<WitchPassiveData>.Instance);
        }
        await ReadData();
        SetAllPassiveData();
    }

    void SetAllPassiveData()
    {
        EPlayerType playerType = DataSingleton<PlayerData>.Instance.PlayerType;
        _allPassiveData = new List<PassiveData>(_commonPassiveData.CommonPassiveDatas);
        _allPassiveData.AddRange(_playerPassiveDatas[(int)playerType].PlayerPassiveDatas);
    }

    public void RemovePassive(PassiveData passive)
    {
        _allPassiveData.Remove(passive);
    }
}